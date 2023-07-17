using Triangles.ViewModels.MainWindowViewModels;

namespace Triangles.Views.Windows.MainWindow
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            var dataSource = new MainWindowViewModel();

            this.DataContext = dataSource;
            this.lblMessage.DataBindings.Add(
                new Binding(
                    nameof(this.lblMessage.Text),
                    dataSource,
                    nameof(dataSource.NestingLevelMax),
                    false,
                    DataSourceUpdateMode.OnPropertyChanged
                    )
                );
            int count = 1;
            this.menuItemLoad.Click += (o, e) => dataSource.NestingLevelMax = $"{++count}";


            this.pictureBoxMain.BackColor = Color.FromArgb(205, 255, 204);
            this.pictureBoxMain.DataBindings.Add(
                new Binding(
                    nameof(this.pictureBoxMain.Image),
                    dataSource,
                    nameof(dataSource.Bitmap)
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