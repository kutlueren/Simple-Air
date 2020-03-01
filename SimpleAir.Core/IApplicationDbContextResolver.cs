namespace SimpleAir.Core
{
    /// <summary>
    /// An abstraction to resolve db context for repositories
    /// </summary>
    public interface IApplicationDbContextResolver
    {
        /// <summary>
        /// returns current db context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T as current context</returns>
        T GetCurrentDbContext<T>();
    }
}