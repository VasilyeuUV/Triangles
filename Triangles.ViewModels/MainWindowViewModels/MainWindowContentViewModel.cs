using Triangles.Models.Creators.GeometryCreators;
using Triangles.Models.Geometry.Triangles;
using Triangles.Models.OtherModels;

namespace Triangles.ViewModels.MainWindowViewModels
{
    /// <summary>
    /// Вьюмодель содержимого главного окна
    /// </summary>
    public class MainWindowContentViewModel : AViewModelBase
    {
        private SquareableCreator _squareableCreator;           // - создатель площадных фигур

        private IEnumerable<TriangleModel>? _triangles;         // - список треугольников
        private InputCoordinatesDataModel _inputData;           // - входные данные


        /// <summary>
        /// CTOR
        /// </summary>
        public MainWindowContentViewModel()
        {
            _squareableCreator = new SquareableCreator();
        }



        /// <summary>
        /// Список треугольников
        /// </summary>
        public IEnumerable<TriangleModel>? Triangles
        { 
            get => _triangles; 
            set => _triangles = value; 
        }

        /// <summary>
        /// Входные данные
        /// </summary>
        public InputCoordinatesDataModel InputData 
        {
            get => _inputData; 
            set => _inputData = value; 
        }
    }
}
