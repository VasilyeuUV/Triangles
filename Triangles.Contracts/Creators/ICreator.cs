namespace Triangles.Contracts.Creators
{

    /// <summary>
    /// Контрак создателя
    /// </summary>
    public interface ICreator
    {

        /// <summary>
        /// Создать объект
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coords"></param>
        /// <returns></returns>
        T Create<T>(IEnumerable<int> coords)
            where T : new();
    }
}
