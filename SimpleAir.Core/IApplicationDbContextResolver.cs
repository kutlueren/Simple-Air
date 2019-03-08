namespace SimpleAir.Core
{
    public interface IApplicationDbContextResolver
    {
        T GetCurrentDbContext<T>();
    }
}
