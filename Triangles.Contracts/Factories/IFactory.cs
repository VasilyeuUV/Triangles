namespace Triangles.Contracts.Factories
{
    /// <summary>
    /// Контракт фабрики, которая может создавать любой объект, зарегистрированный в контейнере зависимостей
    /// </summary>
    /// <typeparam name="TResult">Возвразщаемый тип (ковариантный generic-тип)</typeparam>
    public interface IFactory<out TResult>
    {
        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <returns></returns>
        TResult Create();
    }
}
