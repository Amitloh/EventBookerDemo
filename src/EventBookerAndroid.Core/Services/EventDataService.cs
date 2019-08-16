using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventBookerAndroid.Core.Interfaces;
using EventBookerAndroid.Core.Models;

namespace EventBookerAndroid.Core.Services
{
    public class EventDataService : IEventDataService
    {
        private IEventRepository _eventRepository;

        public EventDataService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllEvents();
        }
        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            return await _eventRepository.GetEventDetails(eventId);
        }

        public async Task<IEnumerable<Event>> SearchEventAsync(int eventId, DateTime date)
        {
            return await _eventRepository.SearchEvent(eventId, date);
        }

        public async Task<Event> SaveNewEventAsync(string eventName, DateTime dateTime, int totalTickets)
        {
            var newEvent = new Event
            {
                EventId = 69420,
                EventName = eventName,
                EventDateTime = dateTime,
                TotalTickets = totalTickets,
                AvailableTickets = totalTickets
            };

            _eventRepository.AddEvent(newEvent);

            return await GetEventByIdAsync(newEvent.EventId);
        }

    }
}
