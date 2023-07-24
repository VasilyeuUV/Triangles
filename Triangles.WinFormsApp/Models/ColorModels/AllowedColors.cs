namespace Triangles.Models.ColorModels;

/// <summary>
/// Диапазон допустимых цветов
/// </summary>
public class AllowedColors
{

    public AllowedColors()
    {
        LightestHsb = new HsbColorModel
        {
            Hue = 90,
            Saturation = 0.3f,
            Brightness = 1f,
        };
        DarkestHsb = new HsbColorModel
        {
            Hue = 150,
            Saturation = 1f,
            Brightness = 0.3f,
        };
    }



    /// <summary>
    /// Самый светлый цвет
    /// </summary>
    public HsbColorModel? LightestHsb { get; set; }


    /// <summary>
    /// Самый тёмный цвет
    /// </summary>
    public HsbColorModel? DarkestHsb { get; set; }


    /// <summary>
    /// Цвета, которые допустимо использовать
    /// </summary>
    public Color[]? UsedColors { get; set; }

}
