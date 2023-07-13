namespace Triangles.WinFormsApp.Services.UserDialogServices
{
    /// <summary>
    /// Контракт сервиса, реализующего диалог с пользователем
    /// </summary>
    public interface IUserDialog
    {

        /// <summary>
        /// Открыть файл
        /// </summary>
        /// <param name="title">Заголовок</param>
        /// <param name="openedFile">Название возвращаемого файла</param>
        /// <param name="filter">Фильтр для выбора типов файлов</param>
        /// <returns></returns>
        bool OpenFile(string title, out string? openedFile, string filter = "All files (*.*) | *.*");


        /// <summary>
        /// Информационное сообщение
        /// </summary>
        /// <param name="msg">Текст сообщения</param>
        /// <param name="title">Заголовок сообщения</param>
        void ShowInformation(string title, string msg);


        /// <summary>
        /// Предупреждающее сообщение
        /// </summary>
        /// <param name="msg">Текст сообщения</param>
        /// <param name="title">Заголовок сообщения</param>
        void ShowWarning(string title, string msg);


        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        /// <param name="msg">Текст сообщения</param>
        /// <param name="title">Заголовок сообщения</param>
        void ShowError(string title, string msg);


        /// <summary>
        /// Согласие пользователя
        /// </summary>
        /// <param name="msg">Текст сообщения</param>
        /// <param name="title">Заголовок сообщения</param>
        /// <param name="exclamation"></param>
        /// <returns></returns>
        bool ShowConfirm(string title, string msg, bool exclamation = false);
    }
}