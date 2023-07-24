using Triangles.WinFormsApp.Properties;

namespace Triangles.WinFormsApp.Models.Geometry.Triangles
{
    /// <summary>
    /// Модель входных данных
    /// </summary>
    internal class InputTriangleDataModel
    {
        private IEnumerable<int[]> _coordinates;


        /// <summary>
        /// CTOR
        /// </summary>
        public InputTriangleDataModel(string[] lines)
        {
            this._coordinates = ParseCoordinates(lines);
        }


        /// <summary>
        /// Список координат
        /// </summary>
        public IEnumerable<int[]> Coordinates => _coordinates;



        /// <summary>
        /// Именованный итератор
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private IEnumerable<int[]> ParseCoordinates(string[] lines)
        {
            if (lines == null
                || lines.Length < 2
                || !int.TryParse(lines[0].Trim(), out int figureCount)
                || figureCount != lines.Length - 1
                )
                throw new InvalidDataException(strings.InvalidInputData);


            foreach (var line in lines.Skip(1))
            {
                int value = 0;
                var result = line
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(item => int.TryParse(item, out value))
                    .Select(x => value)
                    .ToArray();
                yield return result;
            }
        }
    }
}
