using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventBookerAndroid.Core.Models;

namespace EventBookerAndroid.Core.Interfaces
{
    public interface IEventDataService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<IEnumerable<Event>> SearchEventAsync(int eventId, DateTime date);
        Task<Event> GetEventByIdAsync(int eventId);
        Task<Event> SaveNewEventAsync(string eventName, DateTime dateTime, int totalTickets);
    }
}
