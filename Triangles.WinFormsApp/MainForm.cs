using Triangles.ViewModels.MainWindowViewModels;
using Triangles.WinFormsApp.Properties;

namespace Triangles.WinFormsApp
{
    public partial class MainForm : Form
    {
        private readonly MainWindowViewModel _dataSource;


        /// <summary>
        /// CTOR
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            this._dataSource = new MainWindowViewModel();
            AddBindings();
        }


        /// <summary>
        /// Настройка связей
        /// </summary>
        private void AddBindings()
        {
            this.DataContext = _dataSource;
            this.lblMessage.DataBindings.Add(
                new Binding(
                    nameof(this.lblMessage.Text),
                    _dataSource,
                    nameof(_dataSource.NestingLevelMax),
                    false,
                    DataSourceUpdateMode.OnPropertyChanged
                    )
                );


            this.menuItemLoad.DataBindings.Add(
                new Binding(
                    nameof(this.menuItemLoad.Command),
                    _dataSource,
                    nameof(_dataSource.OpenFileCommand),
                    true
                    )
                );


            this.pictureBoxMain.BackColor = Color.FromArgb(205, 255, 204);
            this.pictureBoxMain.DataBindings.Add(
                new Binding(
                    nameof(this.pictureBoxMain.Image),
                    _dataSource,
                    nameof(_dataSource.Bitmap),
                    false,
                    DataSourceUpdateMode.OnPropertyChanged
                    )
                );
        }


        //#################################################################################################################
        #region Events

        /// <summary>
        /// On form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// Закрыть приложение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion // Events
    }
}