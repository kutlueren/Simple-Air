using System.Threading.Tasks;

namespace SimpleAir.Core
{
    public interface IApplicationDbContext
    {
        void SaveChanges();

        Task SaveChangesAsync();
    }
}
