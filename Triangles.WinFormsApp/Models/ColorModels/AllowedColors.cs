namespace Triangles.Models.ColorModels;

/// <summary>
/// Диапазон допустимых цветов
/// </summary>
public class AllowedColors
{


    /// <summary>
    /// Самый светлый цвет
    /// </summary>
    public HsbColorModel? LightestColor { get; set; }


    /// <summary>
    /// Самый тёмный цвет
    /// </summary>
    public HsbColorModel? DarkestColor { get; set; }

    /// <summary>
    /// Цвета, которые допустимо использовать
    /// </summary>
    public HsbColorModel[]? UsedColors { get; set; }


}
