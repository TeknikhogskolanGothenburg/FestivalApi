using System.Threading.Tasks;
using festival_api.Models;

namespace festival_api.Services
{
    public interface IEventRepository: IRepository
    {
         Task<Event[]> GetEvents(bool includeGigs = false);
         Task<Event> GetEvent(int eventId, bool includeGigs = false);

    }
}