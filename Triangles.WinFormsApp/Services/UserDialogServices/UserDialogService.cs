namespace Triangles.WinFormsApp.Services.UserDialogServices
{
    /// <summary>
    /// Диалог с пользоватеем
    /// </summary>
    internal class UserDialogService : IUserDialog
    {
        public bool OpenFile(
            string title, 
            out string? openedFile, 
            string filter = "Все файлы (*.*) | *.*"
            )
        {
            var fileDialog = new OpenFileDialog()
            {
                Title = title,
                Filter = filter
            };

            if (fileDialog.ShowDialog() == DialogResult.Cancel)
            {
                openedFile = null;
                return false;
            }
            openedFile = fileDialog.FileName;
            return true;
        }


        public bool ShowConfirm(string title, string msg, bool exclamation = false)
            => MessageBox.Show(
                msg,
                title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
            ) == DialogResult.Yes;

        public void ShowError(string title, string msg) 
            => MessageBox.Show(
                msg,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
                );

        public void ShowInformation(string title, string msg) 
            => MessageBox.Show(
                msg,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
                );

        public void ShowWarning(string title, string msg) 
            => MessageBox.Show(
                msg,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
                );
    }
}
