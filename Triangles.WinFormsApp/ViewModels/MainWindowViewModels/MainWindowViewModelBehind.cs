using Triangles.Models.ColorModels;
using Triangles.Models.Geometry.Triangles;
using Triangles.WinFormsApp.Models.Geometry.Triangles;
using Triangles.WinFormsApp.Properties;
using Triangles.WinFormsApp.Services.GeometryServices;

namespace Triangles.ViewModels.MainWindowViewModels
{
    /// <summary>
    /// Скрытый функционал вьюмодели главного окна
    /// </summary>
    public partial class MainWindowViewModel
    {

        //int[,] arr22;



        /// <summary>
        /// Получение координат треугольников
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task GetTrianglesCoordsAsync()
        {
            UserDialogService.OpenFile(
                strings.SelectTrianglesCoordsFile,
                out string? filePath,
                "Text files (*.txt) | *.txt"
                );

            try
            {
                string text = await ReadTextAsync(filePath);
                var inputData = new InputTriangleDataModel(text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
                InitAllowedColors(inputData.Coordinates.Count());

                var orderedTriangles = GeometryService.GetTriangles(inputData.Coordinates);
                var commonMatrix = GetCommonMatrix(orderedTriangles);
                if (commonMatrix == null)
                    throw new InvalidDataException("Common matrix is null");

                NestingLevelMax = commonMatrix
                    .Cast<int>()
                    .Max()
                    .ToString();

                Bitmap bitmap = new Bitmap(_BITMAP_WIDTH_MAX, _BITMAP_HEIGHT_MAX);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.Transparent);
                    foreach (var triangle in orderedTriangles)
                        g.FillPolygon(new SolidBrush(triangle.FillColor.Value), triangle.Coordinates);
                }
                Bitmap = bitmap;
            }
            catch (FileNotFoundException ex)
            {
                UserDialogService.ShowError(strings.ErrMsgOpenFileTitle, ex.Message);
                NestingLevelMax = strings.Error;
            }
            catch (Exception ex)
            {
                UserDialogService.ShowError(strings.ErrMsgTitle, ex.Message);
                NestingLevelMax = strings.Error;
            }
        }



        private int[,] GetCommonMatrix(IEnumerable<TriangleModel> orderedTriangles)
        {
            var maxX = (int)orderedTriangles.Max(p => p.Coordinates.Max(c => c.X));
            var maxY = (int)orderedTriangles.Max(p => p.Coordinates.Max(c => c.Y));
            var commonMatrix = new int[maxX, maxY];

            int maxLevel = 0;
            foreach (var triangle in orderedTriangles)
            {
                SummArray(commonMatrix, triangle.BitmapMask);
                maxLevel = commonMatrix.Cast<int>().Max();
                triangle.FillColor = HsbToRgb(_allowedColors.UsedColors[maxLevel - 1]);
            }

            return commonMatrix;
        }




        private Color? HsbToRgb(HsbColorModel hsbColorModel)
        {
            int hi = (int)Math.Floor(hsbColorModel.Hue / 60.0) % 6;
            double f = hsbColorModel.Hue / 60 - Math.Floor((double)hsbColorModel.Hue / 60);

            var value = hsbColorModel.Brightness * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - hsbColorModel.Saturation));
            int q = Convert.ToInt32(value * (1 - f * hsbColorModel.Saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * hsbColorModel.Saturation));

            return hi switch
            {
                0 => Color.FromArgb(255, v, t, p),
                1 => Color.FromArgb(255, q, v, p),
                2 => Color.FromArgb(255, p, v, t),
                3 => Color.FromArgb(255, p, q, v),
                4 => Color.FromArgb(255, t, p, v),
                _ => Color.FromArgb(255, v, p, q),
            };
        }




        private void SummArray(int[,] commonMatrix, int[,] bitmapMask)
        {
            for (int i = 0; i < bitmapMask.GetLength(0); i++)
                for (int j = 0; j < bitmapMask.GetLength(1); j++)
                {
                    commonMatrix[i, j] += bitmapMask[i, j];
                }
        }





        /// <summary>
        /// Инициализация разрешенных цветов
        /// </summary>
        /// <param name="v"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void InitAllowedColors(int count)
        {
            _allowedColors = new AllowedColors()
            {
                LightestColor = new HsbColorModel
                {
                    Hue = 90,
                    Saturation = 0.3f,
                    Brightness = 1f,
                },
                DarkestColor = new HsbColorModel
                {
                    Hue = 150,
                    Saturation = 1f,
                    Brightness = 0.3f,
                }
            };
            _allowedColors.UsedColors = SelectColors(count)?.ToArray();
        }


        /// <summary>
        /// Выбор цветов для использования
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private IEnumerable<HsbColorModel>? SelectColors(int count)
        {
            var hueDiap = _allowedColors?.DarkestColor?.Hue - _allowedColors?.LightestColor?.Hue;
            if (hueDiap == null
                || hueDiap < 1
                )
                throw new ArgumentNullException(nameof(hueDiap));

            var propertiesCount = typeof(HsbColorModel).GetProperties().Count();
            var percentStep = Math.Round((float)(hueDiap / count * 100M / hueDiap) * propertiesCount, 2);

            int hue = _allowedColors?.LightestColor?.Hue ?? 0;
            float saturation = _allowedColors?.LightestColor?.Saturation ?? 0f;
            float brightness = _allowedColors?.LightestColor?.Brightness ?? 0f;

            var hueStep = hueDiap * percentStep / 100;
            var saturationStep = (_allowedColors?.DarkestColor?.Saturation - _allowedColors?.LightestColor?.Saturation) * percentStep / 100 ?? 0f;
            var brightnessStep = (_allowedColors?.DarkestColor?.Brightness - _allowedColors?.LightestColor?.Brightness) * percentStep / 100 ?? 0f;

            List<HsbColorModel> usedColors = new List<HsbColorModel>();
            for (int i = 0, j = 1; i < count; i++)
            {
                if (i == j)
                    hue += (int)hueStep;
                if (i == j + 1)
                    saturation += (float)saturationStep;
                if (i == j + 2)
                {
                    brightness += (float)brightnessStep;
                    j = i + 1;
                }

                usedColors.Add(new HsbColorModel
                {
                    Hue = hue,
                    Saturation = saturation,
                    Brightness = brightness
                });
            }
            return usedColors;
        }




        /// <summary>
        /// Чтение из файла
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<string> ReadTextAsync(string? filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            string text = string.Empty;
            using (StreamReader reader = new StreamReader(filePath))
            {
                text = await reader.ReadToEndAsync();
            }
            return text;
        }
    }
}
