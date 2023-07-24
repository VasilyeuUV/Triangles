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
                triangle.FillColor = HsbToRgb(AllowedColors.UsedColors[maxLevel - 1]);
            }

            return commonMatrix;
        }



        private Color? HsbToRgb(HsbColorModel hsbColorModel)
        {
            //int hi = (int)Math.Floor(hsbColorModel.Hue / 60.0) % 6;
            //double f = hsbColorModel.Hue / 60 - Math.Floor((double)hsbColorModel.Hue / 60);

            //var value = hsbColorModel.Brightness * 255;
            //int v = Convert.ToInt32(value);
            //int p = Convert.ToInt32(value * (1 - hsbColorModel.Saturation));
            //int q = Convert.ToInt32(value * (1 - f * hsbColorModel.Saturation));
            //int t = Convert.ToInt32(value * (1 - (1 - f) * hsbColorModel.Saturation));

            //return hi switch
            //{
            //    0 => Color.FromArgb(255, v, t, p),
            //    1 => Color.FromArgb(255, q, v, p),
            //    2 => Color.FromArgb(255, p, v, t),
            //    3 => Color.FromArgb(255, p, q, v),
            //    4 => Color.FromArgb(255, t, p, v),
            //    _ => Color.FromArgb(255, v, p, q),
            //};

            //return HSBtoRGB(hsbColorModel.Hue, hsbColorModel.Saturation, hsbColorModel.Brightness);
            return FromAhsv(255, hsbColorModel.Hue, hsbColorModel.Saturation, hsbColorModel.Brightness);
        }


        public Color FromAhsv(byte alpha, float hue, float saturation, float value)
        {
            if (hue < 0f || hue > 360f)
                throw new ArgumentOutOfRangeException(nameof(hue), hue, "Hue must be in the range [0,360]");
            if (saturation < 0f || saturation > 1f)
                throw new ArgumentOutOfRangeException(nameof(saturation), saturation, "Saturation must be in the range [0,1]");
            if (value < 0f || value > 1f)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be in the range [0,1]");

            int Component(int n)
            {
                var k = (n + hue / 60f) % 6;
                var c = value - value * saturation * Math.Max(Math.Min(Math.Min(k, 4 - k), 1), 0);
                var b = (int)Math.Round(c * 255);
                return b < 0 ? 0 : b > 255 ? 255 : b;
            }

            return Color.FromArgb(alpha, Component(5), Component(3), Component(1));
        }



        //public static Color HSBtoRGB(float hue, float saturation, float brightness)
        //{
        //    int r = 0, g = 0, b = 0;
        //    if (saturation == 0)
        //    {
        //        r = g = b = (int)(brightness * 255.0f + 0.5f);
        //    }
        //    else
        //    {
        //        float h = (hue - (float)Math.Floor(hue)) * 6.0f;
        //        float f = h - (float)Math.Floor(h);
        //        float p = brightness * (1.0f - saturation);
        //        float q = brightness * (1.0f - saturation * f);
        //        float t = brightness * (1.0f - (saturation * (1.0f - f)));
        //        switch ((int)h)
        //        {
        //            case 0:
        //                r = (int)(brightness * 255.0f + 0.5f);
        //                g = (int)(t * 255.0f + 0.5f);
        //                b = (int)(p * 255.0f + 0.5f);
        //                break;
        //            case 1:
        //                r = (int)(q * 255.0f + 0.5f);
        //                g = (int)(brightness * 255.0f + 0.5f);
        //                b = (int)(p * 255.0f + 0.5f);
        //                break;
        //            case 2:
        //                r = (int)(p * 255.0f + 0.5f);
        //                g = (int)(brightness * 255.0f + 0.5f);
        //                b = (int)(t * 255.0f + 0.5f);
        //                break;
        //            case 3:
        //                r = (int)(p * 255.0f + 0.5f);
        //                g = (int)(q * 255.0f + 0.5f);
        //                b = (int)(brightness * 255.0f + 0.5f);
        //                break;
        //            case 4:
        //                r = (int)(t * 255.0f + 0.5f);
        //                g = (int)(p * 255.0f + 0.5f);
        //                b = (int)(brightness * 255.0f + 0.5f);
        //                break;
        //            case 5:
        //                r = (int)(brightness * 255.0f + 0.5f);
        //                g = (int)(p * 255.0f + 0.5f);
        //                b = (int)(q * 255.0f + 0.5f);
        //                break;
        //        }
        //    }
        //    return Color.FromArgb(Convert.ToByte(255), Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b));
        //}



        //public static Color HsbToRgb(HsbColorModel hsbColorModel) 
        //{ 
        //    //if (0 > a || 255 < a)
        //    //{ 
        //    //    throw new ArgumentOutOfRangeException("a", a, Properties.Resources.InvalidAlpha); 
        //    //}

        //    //if (0f > h || 360f < h) 
        //    //{ 
        //    //    throw new ArgumentOutOfRangeException("h", h, Properties.Resources.InvalidHue); 
        //    //} 

        //    //if (0f > s || 1f < s) 
        //    //{
        //    //    throw new ArgumentOutOfRangeException("s", s, Properties.Resources.InvalidSaturation); 
        //    //}

        //    //if (0f > b || 1f < b) 
        //    //{ 
        //    //    throw new ArgumentOutOfRangeException("b", b, Properties.Resources.InvalidBrightness); 
        //    //} 

        //    //if (0 == s) 
        //    //{
        //    //    return Color.FromArgb(a, Convert.ToInt32(b * 255), Convert.ToInt32(b * 255), Convert.ToInt32(b * 255)); 
        //    //} 

        //    float fMax, fMid, fMin; 
        //    int iSextant, iMax, iMid, iMin;

        //    int a = 255;
        //    float h = hsbColorModel.Hue;
        //    float s = hsbColorModel.Saturation;
        //    float b = hsbColorModel.Brightness;

        //    if (0.5 < b) 
        //    {
        //        fMax = b - (b * s) + s;
        //        fMin = b + (b * s) - s; 
        //    } 
        //    else 
        //    {
        //        fMax = b + (b * s); 
        //        fMin = b - (b * s); }
        //    iSextant = (int)Math.Floor(h / 60f);

        //    if (300f <= h) 
        //    {
        //        h -= 360f; 
        //    }

        //    h /= 60f; 
        //    h -= 2f * (float)Math.Floor(((iSextant + 1f) % 6f) / 2f);

        //    if (0 == iSextant % 2)
        //    {
        //        fMid = h * (fMax - fMin) + fMin;
        //    } 
        //    else
        //    { 
        //        fMid = fMin - h * (fMax - fMin);
        //    } 

        //    iMax = Convert.ToInt32(fMax * 255);
        //    iMid = Convert.ToInt32(fMid * 255);
        //    iMin = Convert.ToInt32(fMin * 255); 

        //    switch (iSextant) 
        //    { 
        //        case 1: return Color.FromArgb(a, iMid, iMax, iMin); 
        //        case 2: return Color.FromArgb(a, iMin, iMax, iMid);
        //        case 3: return Color.FromArgb(a, iMin, iMid, iMax);
        //        case 4: return Color.FromArgb(a, iMid, iMin, iMax); 
        //        case 5: return Color.FromArgb(a, iMax, iMin, iMid);
        //        default: return Color.FromArgb(a, iMax, iMid, iMin);
        //    } 
        //}



        ///// <summary>
        ///// Converts to RGB.
        ///// </summary>
        ///// <param name="hsb">The HSB.</param>
        ///// <returns></returns>
        //internal static Color? ConvertToRGB(HsbColorModel hsb)
        //{
        //    double chroma = hsb.Saturation * hsb.Brightness;
        //    double hue2 = hsb.Hue / 60;
        //    double x = chroma * (1 - Math.Abs(hue2 % 2 - 1));
        //    double r1 = 0d;
        //    double g1 = 0d;
        //    double b1 = 0d;

        //    if (hue2 >= 0 && hue2 < 1)
        //    {
        //        r1 = chroma;
        //        g1 = x;
        //    }
        //    else if (hue2 >= 1 && hue2 < 2)
        //    {
        //        r1 = x;
        //        g1 = chroma;
        //    }
        //    else if (hue2 >= 2 && hue2 < 3)
        //    {
        //        g1 = chroma;
        //        b1 = x;
        //    }
        //    else if (hue2 >= 3 && hue2 < 4)
        //    {
        //        g1 = x;
        //        b1 = chroma;
        //    }
        //    else if (hue2 >= 4 && hue2 < 5)
        //    {
        //        r1 = x;
        //        b1 = chroma;
        //    }
        //    else if (hue2 >= 5 && hue2 <= 6)
        //    {
        //        r1 = chroma;
        //        b1 = x;
        //    }

        //    double m = hsb.Brightness - chroma;

        //    return Color.FromArgb(r1 + m, g1 + m, b1 + m);
        //    //{                
        //    //    R = r1 + m,
        //    //    G = g1 + m,
        //    //    B = b1 + m
        //    //});
        //}


        //private Color? HsbToRgb(HsbColorModel hsbColorModel)
        //{
        //    if (hsbColorModel.Hue == 0)
        //        return Color.FromArgb(hsbColorModel.Brightness, hsbColorModel.Brightness, hsbColorModel.Brightness);
        //}

        //public static void ConvertHSBToRGB(float h, float s, float v, out float r, out float g, out float b)
        //{
        //    if (s == 0f)
        //    {
        //        // if s = 0 then h is undefined
        //        r = v;
        //        g = v;
        //        b = v;
        //    }
        //    else
        //    {
        //        float hue = (float)h;
        //        if (h == 360.0f)
        //        {
        //            hue = 0.0f;
        //        }
        //        hue /= 60.0f;
        //        int i = (int)Math.Floor((double)hue);
        //        float f = hue - i;
        //        float p = v * (1.0f - s);
        //        float q = v * (1.0f - (s * f));
        //        float t = v * (1.0f - (s * (1 - f)));

        //        switch (i)
        //        {
        //            case 0: r = v; g = t; b = p; break;
        //            case 1: r = q; g = v; b = p; break;
        //            case 2: r = p; g = v; b = t; break;
        //            case 3: r = p; g = q; b = v; break;
        //            case 4: r = t; g = p; b = v; break;
        //            case 5: r = v; g = p; b = q; break;

        //            default: r = 0.0f; g = 0.0f; b = 0.0f; break; /*Trace.Assert(false);*/ // hue out of range
        //        }
        //    }
        //}




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
            AllowedColors = new AllowedColors()
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
            AllowedColors.UsedColors = SelectColors(count)?.ToArray();
        }


        /// <summary>
        /// Выбор цветов для использования
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private IEnumerable<HsbColorModel>? SelectColors(int count)
        {
            var hueDiap = AllowedColors?.DarkestColor?.Hue - AllowedColors?.LightestColor?.Hue;
            if (hueDiap == null
                || hueDiap < 1
                )
                throw new ArgumentNullException(nameof(hueDiap));

            var propertiesCount = typeof(HsbColorModel).GetProperties().Count();
            var percentStep = Math.Round((float)(hueDiap / count * 100M / hueDiap) * propertiesCount, 2);

            int hue = AllowedColors?.LightestColor?.Hue ?? 0;
            float saturation = AllowedColors?.LightestColor?.Saturation ?? 0f;
            float brightness = AllowedColors?.LightestColor?.Brightness ?? 0f;

            var hueStep = hueDiap * percentStep / 100;
            var saturationStep = (AllowedColors?.DarkestColor?.Saturation - AllowedColors?.LightestColor?.Saturation) * percentStep / 100 ?? 0f;
            var brightnessStep = (AllowedColors?.DarkestColor?.Brightness - AllowedColors?.LightestColor?.Brightness) * percentStep / 100 ?? 0f;

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
