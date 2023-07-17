using System.Drawing;
using Triangles.Models.Extensions;

namespace Triangles.Models.Geometry
{
    /// <summary>
    /// 2D геометрическая фигура
    /// </summary>
    public abstract class AGeometricFigure2DBase
    {
        private const int _DIMENSION = 2;                                   // - количесвто измерений для 2D

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="coords">Значения координат в виде масиива </param>
        protected AGeometricFigure2DBase(IEnumerable<int> coords)
        {
            this.Coordinates = AddCoordinates(coords).ToArray();
            this.BitmapMask = CreateBitmapMask(this.Coordinates);
        }



        /// <summary>
        /// Координаты геометрической фигуры на плоскости
        /// </summary>
        public Point[] Coordinates { get; }


        public int[,] BitmapMask { get; set; }




        /// <summary>
        /// Валидация фигуры
        /// </summary>
        /// <returns></returns>
        protected abstract bool CheckValidation();


        /// <summary>
        /// Создать и добавить координаты точек
        /// </summary>
        /// <param name="coords"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static IEnumerable<Point> AddCoordinates(IEnumerable<int> coords)
        {
            foreach (var coord in coords.Split(_DIMENSION))
            {
                if (coord.Count() == _DIMENSION)
                    yield return new Point(coord.First(), coord.Last());
            }
        }


        /// <summary>
        /// Создание маски размещения фигуры на плоскости
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private int[,] CreateBitmapMask(Point[] coordinates)
        {
            var maxX = (int)coordinates.Max(coord => coord.X);
            var maxY = (int)coordinates.Max(coord => coord.Y);
            var bitmap = new Bitmap(maxX, maxY);
            var intRectangle = new int[maxX * maxY];

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);
                g.FillPolygon(new SolidBrush(Color.Green), coordinates);
            }

            var imageConverter = new ImageConverter();
            var byteArray = (byte[]?)imageConverter.ConvertTo(bitmap, typeof(byte[]));

            for (int counter = 0, k = 0; counter < byteArray?.Length; counter += 4, k++)
            {
                if (byteArray[counter] != 0               // blue
                    || byteArray[counter + 1] != 0        // green
                    || byteArray[counter + 2] != 0        // red
                    )
                    intRectangle[k] = 1;
            }

            int[,] mask = new int[maxX, maxY];
            int x = 0, y = 0;
            foreach (var item in intRectangle)
            {
                mask[x, y] = item;

                if (++x >= maxX)
                {
                    x = 0;
                    ++y;
                }
            }
            return mask;
        }
    }
}
