using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Triangles.ViewModels
{
    public abstract class AViewModelBase : INotifyPropertyChanged, IDisposable
    {

        /// <summary>
        /// Обработчик события
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        /// <summary>
        /// Сеттер. Задача - обновлять значение свойства, для которого определено поле
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">ссылка на поле свойства</param>
        /// <param name="value">новое значение</param>
        /// <param name="propertyName">имя свойства</param>
        /// <returns></returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        //###################################################################################################################
        #region SetValue // Расширение сеттера

        /// <summary>
        /// Установки свойства
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="field">Старое значение</param>
        /// <param name="value">Новое значение</param>
        /// <param name="propertyName">Изменяемое свойство</param>
        /// <returns>Результат значения установки свойства/returns>
        protected SetValueResult<T> SetValue<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value))
                return new SetValueResult<T>(false, field, value, this);

            field = value;
            OnPropertyChanged(propertyName!);
            return new SetValueResult<T>(true, field, value, this);
        }


        /// <summary>
        /// Результат значения установки свойства
        /// </summary>
        public readonly ref struct SetValueResult<T>
        {
            private readonly bool _result;
            private readonly T _oldValue;
            private readonly T _newValue;
            private readonly AViewModelBase _viewModel;

            /// <summary>
            /// CTOR
            /// </summary>
            /// <param name="result">Результат: было установлено свойство или нет</param>
            /// <param name="oldValue">Старое значение</param>
            /// <param name="newValue">Новое значение</param>
            /// <param name="viewModel">текущая ViewModel</param>
            public SetValueResult(bool result, T oldValue, T newValue, AViewModelBase viewModel)
            {
                this._result = result;
                this._oldValue = oldValue;
                this._newValue = newValue;
                this._viewModel = viewModel;
            }

            // ДАЛЕЕ - МЕТОДЫ (ДОПОЛНИТЕЛЬНЫЕ ДЕЙСТВИЯ), КОТОРЫЕ МОЖНО ВЫПОЛНЯТЬ ПРИ ИЗМЕНЕНИИ СВОЙСТВА (СВЯЗАННЫЕ С ЭТИМ СВОЙСТВОМ)

            /// <summary>
            /// Метод может выполнить некоторые действия
            /// </summary>
            /// <param name="action">Действия с новым значением</param>
            /// <returns>Возвращает результат изменения свойства</returns>
            public SetValueResult<T> Then(Action<T> action)
            {
                if (_result) { action(_newValue); }
                return this;
            }
            // ПРИМЕНЕНИЕ:
            // set => SetValue(ref Поле, valur)
            //       .Then(v => OnPropertyChanged(nameof(Зависимое_от_Поля_Свойство)))
            //       .Then(...);
            // Зависимое_от_Поля_Свойство - то, которое нужно обновить при изменении значения Поля


            /// <summary>
            /// Действие будет выполнено по условию
            /// </summary>
            /// <param name="isValue">Условие, при котором выполнится действие (true)</param>
            /// <param name="action">Действие</param>
            /// <returns></returns>
            public SetValueResult<T> ThenIf(Func<T, bool> isValue, Action<T> action)
            {
                if (_result && isValue(_newValue)) { action(_newValue); }
                return this;
            }
            // ПРИМЕНЕНИЕ:
            // set => SetValue(ref Поле, valur)
            //       .UpdateProperty(nameof(Зависимое_от_Поля_Свойство))
            //       .Then(...)
            //       .ThenIf(v => !string.IsNullOrEmpty(v), v => Debug.WriteLine("не пустое значение"));


            /// <summary>
            /// Указывает имя свойства, которое нужно обновить при изменении текущего свойства
            /// </summary>
            /// <param name="propertyName">имя свойства, которое нужно обновить</param>
            /// <returns>Возвращает результат изменения свойства</returns>
            public SetValueResult<T> UpdateProperty(string propertyName)
            {
                if (_result) { _viewModel.OnPropertyChanged(propertyName); }
                return this;
            }
            // ПРИМЕНЕНИЕ:
            // set => SetValue(ref Поле, value)
            //       .UpdateProperty(nameof(Зависимое_от_Поля_Свойство))
            //       .Then(...);
            // Зависимое_от_Поля_Свойство - то, которое нужно обновить при изменении значения Поля

        }

        #endregion // SetValue



        //###################################################################################################################
        #region INotifyPropertyChanged


        public event PropertyChangedEventHandler? PropertyChanged;


        #endregion // INotifyPropertyChanged



        //###################################################################################################################
        #region IDisposable

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
                return;

            _disposed = true;

            // Далее - освобождение управляемых ресурсов

        }

        #endregion // IDisposable

    }
}
