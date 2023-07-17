using Autofac;

namespace Triangles.ViewModels.Extensions
{
    /// <summary>
    /// Расширение для регистрации вьюмоделей
    /// </summary>
    public static class ContainerBuilderExtensions
    {

        /// <summary>
        /// Обобщённый метод регистрация вьюмодели
        /// </summary>
        /// <typeparam name="TImplementation">Класс реализации</typeparam>
        /// <typeparam name="TInterface">Интерфейс контракта</typeparam>
        /// <param name="builder">Контейнер зависимостей</param>
        public static void RegisterViewModel<TImplementation, TInterface>(this ContainerBuilder builder)
            where TInterface : notnull
            where TImplementation : TInterface                  // - generic-тип для реализации должен реализовывать generic-тип контракта (защита от передачи несовместимых типов)
        {
            builder.RegisterType<TImplementation>()             // - регистрируемый тип
                .As<TInterface>()                               // - контрак вьюмодели (интерфейс, под которым регистрируем тип)
                .InstancePerDependency()                        // - время жизни вьюмодели (для каждого запроса будет создаваться новый экземпляр вьюмодели)
                .ExternallyOwned();                             // - не освобождать автоматически при завершении программы (Вьюмодель должна освобождаться явным образом)                   
        }
    }
}
