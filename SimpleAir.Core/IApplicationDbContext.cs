using System.Threading.Tasks;

namespace SimpleAir.Core
{
    /// <summary>
    /// An interface to abstact db context for its references. Consequently it abstracts the underlying data access technology such as EF, ADO.NET and the class which uses IApplicationDbContext doesn't depend of that technology
    /// </summary>
    public interface IApplicationDbContext
    {
        /// <summary>
        /// Commits the changes
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Commits the changes async
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}