using System.Drawing;

namespace Triangles.Models.Geometry.Triangles
{
    /// <summary>
    /// Модель треугольника
    /// </summary>
    public class TriangleModel : APolygon 
    {
        private const int _APEXESCOUNT = 3;                             // - количество вершин фигуры

        // - выршины треугольника, определенные при создании
        private Point _triangleApexA;
        private Point _triangleApexB;
        private Point _triangleApexC;


        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="coords"></param>
        public TriangleModel(IEnumerable<int> coords) : base(coords)
        {
            if (!CheckValidation()
                || CheckCollinear()
                )
                throw new InvalidDataException("At the specified coordinates, the figure cannot be built");
        }


        /// <summary>
        /// Координаты вершины А треугольника
        /// </summary>
        public Point A
        {
            get
            {
                if (_triangleApexA == default)
                    _triangleApexA = new Point((Size)Coordinates[0]);
                return _triangleApexA;
            }
        }

        /// <summary>
        /// Координаты вершины B треугольника
        /// </summary>
        public Point B
        {
            get
            {
                if (_triangleApexB == default)
                    _triangleApexB = new Point((Size)Coordinates[1]);
                return _triangleApexB;
            }
        }

        /// <summary>
        /// Координаты вершины C треугольника
        /// </summary>
        public Point C
        {
            get
            {
                if (_triangleApexC == default)
                    _triangleApexC = new Point((Size)Coordinates[2]);
                return _triangleApexC;
            }
        }



        //####################################################################################################################
        #region ASquareableGeometricFigureBase

        protected override bool CheckValidation()
        {
            return Coordinates.Length == _APEXESCOUNT;
        }



        protected override double GetSquare()
        {
            return Math.Abs((B.X - A.X) * (C.Y - A.Y) - (C.X - A.X) * (B.Y - A.Y)) / 2;
        }

        #endregion // ASquareableGeometricFigureBase
    }
}
