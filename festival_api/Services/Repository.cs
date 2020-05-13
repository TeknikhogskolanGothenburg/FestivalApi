using System.Threading.Tasks;
using festival_api.DB_Context;
using Microsoft.Extensions.Logging;

namespace festival_api.Services
{

    
    public class Repository : IRepository
    {
        protected readonly FestivalDbContext _festivalContext;
        protected readonly ILogger<Repository> _logger;

        public Repository(FestivalDbContext context, ILogger<Repository> logger)
        {
            this._festivalContext = context;
            _logger = logger;
        }
        
        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding object of type {entity.GetType()}");
            _festivalContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Deleting object of type {entity.GetType()}");
            _festivalContext.Remove(entity);
        }

        public async Task<bool> Save()
        {
            _logger.LogInformation("Saving changes");
            return (await _festivalContext.SaveChangesAsync()) >= 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating object of type {entity.GetType()}");
            _festivalContext.Update(entity);
        }
    }
}