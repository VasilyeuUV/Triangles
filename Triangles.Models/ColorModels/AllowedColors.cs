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
    /// Запрещенные цветовые диапазоны
    /// </summary>
    public IEnumerable<AllowedColors>? ProhibitedColorRanges { get; set; }

}
