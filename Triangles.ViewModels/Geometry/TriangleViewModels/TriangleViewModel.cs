using System.Drawing;
using Triangles.Models.Geometry;

namespace Triangles.ViewModels.Geometry.TriangleViewModels
{
    /// <summary>
    /// Вьюмодель коллекции треугольников
    /// </summary>
    public class TriangleViewModel : AViewModelBase, ITriangleCollectionViewModel
    {
        private string? _nestingLevelMax;
        private readonly Bitmap? _bitmap;


        /// <summary>
        /// CTOR
        /// </summary>
        public TriangleViewModel(IEnumerable<AGeometricFigure2DBase> _geometryObjects)
        {

        }


        /// <summary>
        /// Свойство для элементов коллекции (только перебор)
        /// </summary>
        public Bitmap? Bitmap => _bitmap; 


        /// <summary>
        /// Максимальный уровень вложенности
        /// </summary>
        public string? NestingLevelMax
        {
            get => _nestingLevelMax;
            set => Set(ref _nestingLevelMax, value);
        }


        //#############################################################################################################
        #region ITriangleCollectionViewModel

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        #endregion // ITriangleCollectionViewModel

    }
}
