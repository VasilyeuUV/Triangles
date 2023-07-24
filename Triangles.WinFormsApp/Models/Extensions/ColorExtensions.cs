using Triangles.Models.ColorModels;

namespace Triangles.WinFormsApp.Models.Extensions
{
    public static class ColorExtensions
    {
        public static Color ToColor(this HsbColorModel item)
        {
            return HsbToRgb(item);
        }




        private static Color HsbToRgb(HsbColorModel hsbColorModel)
        {
            return HsbToRgb(255, hsbColorModel.Hue, hsbColorModel.Saturation, hsbColorModel.Brightness);
        }


        private static Color HsbToRgb(byte alpha, float hue, float saturation, float value)
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

    }
}
