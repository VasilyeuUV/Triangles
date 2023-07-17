using System.Drawing;
using Triangles.Models.Geometry;

namespace Triangles.ViewModels.BitmapViewModels
{
    public class BitmapViewModel : IBitmapViewModel
    {
        private readonly IEnumerable<AGeometricFigure2DBase> _figures;

        private readonly string? _nestingLevelMax;
        private Bitmap? _bitmap;

        /// <summary>
        /// CTOR
        /// </summary>
        public BitmapViewModel(IEnumerable<AGeometricFigure2DBase> figures)
        {
            this._figures = figures;
        }


        /// <summary>
        /// Свойство для элементов коллекции (только перебор)
        /// </summary>
        public Bitmap? Bitmap => _bitmap;


        /// <summary>
        /// Максимальный уровень вложенности
        /// </summary>
        public string? NestingLevelMaxt => _nestingLevelMax;


        //####################################################################################################
        #region IBitmapViewModel

        public async Task InitializeAsync()
        {
            var maxX = (int)_figures.Max(p => p.Coordinates.Max(coord => coord.X));
            var maxY = (int)_figures.Max(p => p.Coordinates.Max(coord => coord.Y));
            _bitmap = new Bitmap(maxX, maxY);

        }

        #endregion // IBitmapViewModel

    }
}
