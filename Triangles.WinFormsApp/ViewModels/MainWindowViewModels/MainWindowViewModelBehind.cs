using Triangles.Models.ColorModels;
using Triangles.Models.Geometry.Triangles;
using Triangles.WinFormsApp.Models.Extensions;
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
                NestingLevelMax = GetMaxNestingLevel(commonMatrix).ToString();


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
                Bitmap = new Bitmap(_BITMAP_WIDTH_MAX, _BITMAP_HEIGHT_MAX);
            }
            catch (Exception ex)
            {
                UserDialogService.ShowError(strings.ErrMsgTitle, ex.Message);
                NestingLevelMax = strings.Error;
                Bitmap = new Bitmap(_BITMAP_WIDTH_MAX, _BITMAP_HEIGHT_MAX);
            }
        }


        /// <summary>
        /// Получение уровня вложенности
        /// </summary>
        /// <param name="commonMatrix"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private int GetMaxNestingLevel(int[,] commonMatrix)
        {
            return commonMatrix
                .Cast<int>()
                .Max();                              
        }

        private int[,] GetCommonMatrix(IEnumerable<TriangleModel> orderedTriangles)
        {
            var maxX = (int)orderedTriangles.Max(p => p.Coordinates.Max(c => c.X));
            var maxY = (int)orderedTriangles.Max(p => p.Coordinates.Max(c => c.Y));
            var commonMatrix = new int[maxX, maxY];

            int maxLevel = 0;
            foreach (var triangle in orderedTriangles)
            {
                SummArray(commonMatrix, triangle);
                maxLevel = commonMatrix.Cast<int>().Max();
                triangle.FillColor = AllowedColors?.UsedColors?[triangle.NestingLevel - 1];
            }

            return commonMatrix;
        }


        private void SummArray(int[,] commonMatrix, int[,] bitmapMask)
        {
            for (int i = 0; i < bitmapMask.GetLength(0); i++)
                for (int j = 0; j < bitmapMask.GetLength(1); j++)
                {
                    commonMatrix[i, j] += bitmapMask[i, j];
                }
        }


        private void SummArray(int[,] commonMatrix, TriangleModel triangle)
        {
            var triangleNestingLevel = GetMaxNestingLevel(triangle.BitmapMask);
            for (int i = 0; i < triangle.BitmapMask.GetLength(0); i++)
                for (int j = 0; j < triangle.BitmapMask.GetLength(1); j++)
                {
                    commonMatrix[i, j] += triangle.BitmapMask[i, j];
                    if (triangle.BitmapMask[i, j] != 0
                        && commonMatrix[i, j] > triangleNestingLevel)
                        triangleNestingLevel = commonMatrix[i, j];
                }
            triangle.NestingLevel = triangleNestingLevel;
        }



        /// <summary>
        /// Инициализация разрешенных цветов
        /// </summary>
        /// <param name="v"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void InitAllowedColors(int count)
        {
            AllowedColors = new AllowedColors()
            {
                LightestHsb = new HsbColorModel
                {
                    Hue = 90,
                    Saturation = 0.3f,
                    Brightness = 1f,
                },
                DarkestHsb = new HsbColorModel
                {
                    Hue = 150,
                    Saturation = 1f,
                    Brightness = 0.3f,
                }
            };
            AllowedColors.UsedColors = SelectColors(count)?.ToArray();
        }


        /// <summary>
        /// Выбор цветов для использования
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private IEnumerable<Color>? SelectColors(int count)
        {
            var hueDiap = AllowedColors?.DarkestHsb?.Hue - AllowedColors?.LightestHsb?.Hue;
            if (hueDiap == null
                || hueDiap < 1
                )
                throw new ArgumentNullException(nameof(hueDiap));

            var propertiesCount = typeof(HsbColorModel).GetProperties().Count();
            var percentStep = Math.Round((float)(hueDiap / count * 100M / hueDiap) * propertiesCount, 2);

            int hue = AllowedColors?.LightestHsb?.Hue ?? 0;
            float saturation = AllowedColors?.LightestHsb?.Saturation ?? 0f;
            float brightness = AllowedColors?.LightestHsb?.Brightness ?? 0f;

            var hueStep = hueDiap * percentStep / 100;
            var saturationStep = (float)((AllowedColors?.DarkestHsb?.Saturation - AllowedColors?.LightestHsb?.Saturation) * percentStep / 100 ?? 0f);
            var brightnessStep = (float)((AllowedColors?.DarkestHsb?.Brightness - AllowedColors?.LightestHsb?.Brightness) * percentStep / 100 ?? 0f);

            List<Color> usedColors = new List<Color>();
            for (int i = 1, j = 1; i <= count; i++)
            {
                if (i == j)
                    hue += (int)hueStep;
                if (i == j + 1)
                {
                    saturation += saturationStep;
                    if (saturation > 1)
                        saturation = 1;
                }
                if (i == j + 2)
                {
                    brightness += brightnessStep;
                    if (brightness < 0)
                        brightness = 0;
                    j = i + 1;
                }

                var usedHsb = new HsbColorModel
                {
                    Hue = hue,
                    Saturation = saturation,
                    Brightness = brightness
                };

                usedColors.Add(usedHsb.ToColor());
            };

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
