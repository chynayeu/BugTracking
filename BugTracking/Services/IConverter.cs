namespace BugTracking.Services
{
    /// <summary>
    /// Интерфейс для конвертеров
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="F"></typeparam>
    public interface IConverter<T,F>
    {
        /// <summary>
        /// Конвертер для преобразования  из <typeparamref name="F"/> в <typeparamref name="T"/>
        /// </summary>
        /// <param name="source">объект для преобразования</param>
        /// <returns>преобразованный объект</returns>
        public T Convert(F source);

        /// <summary>
        /// Конвертер для преобразования  из <typeparamref name="T"/> в <typeparamref name="F"/>
        /// </summary>
        /// <param name="source">объект для преобразования</param>
        /// <returns>преобразованный объект</returns>
        public F Convert(T source);
    }
}
