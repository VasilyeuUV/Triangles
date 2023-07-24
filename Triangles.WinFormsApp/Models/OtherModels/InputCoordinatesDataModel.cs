namespace Triangles.Models.OtherModels
{
    /// <summary>
    /// Модель входных данных
    /// </summary>
    public class InputCoordinatesDataModel
    {
        private IEnumerable<int[]> _coordinates;


        //#########################################################################################################
        #region CTOR

        public InputCoordinatesDataModel(string str) : this(str.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)) { }

        public InputCoordinatesDataModel(string[] lines)
        {
            this._coordinates = ParseCoordinates(lines);
        }

        #endregion // CTOR




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
                yield break;


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
