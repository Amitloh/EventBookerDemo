using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBookerAndroid.Core.Interfaces;
using EventBookerAndroid.Core.Models;

namespace EventBookerAndroid.Core.Repositories
{
    public class EventRepository : BaseRepository, IEventRepository
    {
        //TODO: Add hardcoded list here for quick self testing only
        public List<Event> AllEvents = new List<Event>()
        {
            new Event
            {
                EventId = 1,
                EventName = "qa conference",
                EventDateTime = DateTime.Now,
                TotalTickets = 10,
                AvailableTickets = 10
            },

            new Event
            {
                EventId = 2,
                EventName = "scrum meeting",
                EventDateTime = DateTime.Now,
                TotalTickets = 10,
                AvailableTickets = 10
            },

            new Event
            {
                EventId = 3,
                EventName = "scrum meeting",
                EventDateTime = DateTime.Now,
                TotalTickets = 10,
                AvailableTickets = 10
            }
        };

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await Task.FromResult(AllEvents);
        }

        public async Task<Event> GetEventDetails(int eventId)
        {
            return await Task.FromResult(AllEvents.FirstOrDefault(e => e.EventId == eventId));
        }

        public async Task<IEnumerable<Event>> SearchEvent(int eventId, DateTime dateTime)
        {
            return await Task.FromResult(AllEvents.Where(e => e.EventDateTime.Day == dateTime.Day));
        }

        public async Task<Event> UpdateEventTickets(int eventId)
        {
            Event updateEvent = await GetEventDetails(eventId);

            updateEvent.AvailableTickets -= 1;

            //TODO: Handle saving and actual event update here. This is for testing

            return await GetEventDetails(eventId);
        }

        public void AddEvent(Event newEvent)
        {
            AllEvents.Add(newEvent);
        }
    }
}
