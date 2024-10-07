using Api.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure;

public abstract class GenericService(ApplicationContext context)
{
    public T? GetInstanceByCode<T>(Guid code, DbSet<T> dbSet) where T : GenericEntity
    {
        return dbSet.Find(code);
    }

    public bool DeleteInstanceByCode<T>(T? instance, DbSet<T> dbSet) where T : GenericEntity
    {
        if (instance != null)
        {
            dbSet.Remove(instance);
            context.SaveChanges();
            return true;
        }
        return false;
    }
}