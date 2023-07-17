using System.Windows.Input;

namespace Triangles.ViewModels.Commands
{
    /// <summary>
    /// Класс универсальной команды (не привязанной к конкретной логической задаче)
    /// т.е. нужно передать команде выполняемую логику извне 
    /// </summary>
    public class Command : ICommand
    {
        private readonly Action _execute;       // - выполняемая логика команды


        /// <summary>
        /// СТОR
        /// </summary>
        /// <param name="execute">Метод для выполнения, переданный извне</param>
        public Command(Action execute)
        {
            this._execute = execute;
        }


        //################################################################################################
        #region ICommand

        public event EventHandler? CanExecuteChanged;


        public bool CanExecute(object? parameter)
        {
            return true;
        }


        public void Execute(object? parameter)
        {
            _execute.Invoke();      // - выполнение логики команды, поступившей извне
        }

        #endregion // ICommand
    }
}
