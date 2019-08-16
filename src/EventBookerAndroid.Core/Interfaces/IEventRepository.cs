using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventBookerAndroid.Core.Models;

namespace EventBookerAndroid.Core.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> SearchEvent(int eventId, DateTime dateTime);
        Task<IEnumerable<Event>> GetAllEvents();
        Task<Event> GetEventDetails(int eventId);
        Task<Event> UpdateEventTickets(int eventId);
        void AddEvent(Event newEvent);
    }
}
