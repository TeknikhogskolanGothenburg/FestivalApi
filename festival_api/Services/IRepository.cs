using System.Threading.Tasks;

namespace festival_api.Services
{
    public interface IRepository
    {
         // Genereic methods
         void Add<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         Task<bool> Save();

    }
}