using System.Windows.Input;
using Triangles.Models.ColorModels;
using Triangles.ViewModels.Commands;
using Triangles.WinFormsApp.Services.UserDialogServices;

namespace Triangles.ViewModels.MainWindowViewModels
{
    /// <summary>
    /// Вьюмодель главного окна со свойсвами для отображения во вью
    /// </summary>
    public partial class MainWindowViewModel : AViewModelBase
    {
        private const int _BITMAP_WIDTH_MAX = 1000;
        private const int _BITMAP_HEIGHT_MAX = 1000;


        private string? _nestingLevelMax;
        private Bitmap? _bitmap;
        private AllowedColors? _allowedColors;

        private readonly AsyncCommand _openFileCommand;          // - команда открытия файла 

        private readonly IUserDialog _userDialogService;
        //private readonly IGeometryService _geometryService; 


        /// <summary>
        /// CTOR
        /// </summary>
        public MainWindowViewModel()
        {
            this._bitmap = new Bitmap(_BITMAP_WIDTH_MAX, _BITMAP_HEIGHT_MAX);
            this._allowedColors = new AllowedColors();

            this._userDialogService = new UserDialogService();

            this._openFileCommand = new AsyncCommand(GetTrianglesCoordsAsync);

        }


        /// <summary>
        /// Максимальный уровень вложенности
        /// </summary>
        public string? NestingLevelMax
        { 
            get => _nestingLevelMax;
            set => Set(ref _nestingLevelMax, value);
        }


        /// <summary>
        /// 
        /// </summary>
        public Bitmap Bitmap
        {
            get => _bitmap;
            set => Set(ref _bitmap, value);
        }


        /// <summary>
        /// Открытие файла
        /// </summary>
        public ICommand OpenFileCommand => _openFileCommand;


        public IUserDialog UserDialogService => _userDialogService;
    }
}
