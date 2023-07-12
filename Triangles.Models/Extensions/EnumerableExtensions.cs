namespace Triangles.Models.Extensions
{
    /// <summary>
    /// Расширения для массивов
    /// </summary>
    public static class EnumerableExtensions
    {

        /// <summary>
        /// Разбить список (массив) на куски определенного размера
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">Список (массив), который подлежит разбиению</param>
        /// <param name="chunkSize">Количество элементов, на которое будет разбит список (массив)</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> items, int chunkSize)
        {
            for (int i = 0; i < items.Count() / chunkSize + 1; i++)
                yield return items
                    .Skip(i * chunkSize)
                    .Take(chunkSize);
        }
    }
}
