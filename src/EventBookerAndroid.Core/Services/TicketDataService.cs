using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventBookerAndroid.Core.Interfaces;
using EventBookerAndroid.Core.Models;

namespace EventBookerAndroid.Core.Services
{
    public class TicketDataService : ITicketDataService
    {
        private ITicketRepository _ticketRepository;
        private IEventRepository _eventRepository;

        public TicketDataService(ITicketRepository ticketRepository, IEventRepository eventRepository)
        {
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
        }
        public async Task<IEnumerable<Ticket>> GetTicketsForEvent(int eventId)
        {
            return await _ticketRepository.GetTickets(eventId);
        }

        public async Task<Ticket> SellTicketToEvent(int eventId, string holderName)
        {
            Event eventOfTicket = await _eventRepository.GetEventDetails(eventId);

            if(eventOfTicket.AvailableTickets > 0)
            {
                var ticket = new Ticket()
                {
                    TicketId = (eventOfTicket.TotalTickets - eventOfTicket.AvailableTickets) + 1,
                    EventId = eventOfTicket.EventId,
                    TicketHolderName = holderName
                };

                _ticketRepository.SaveTicket(ticket);
                await _eventRepository.UpdateEventTickets(eventOfTicket.EventId);
                //Return saved ticket for sanity check
                //TODO: handle differently when data persistence setup, this is just for testing
                return await _ticketRepository.GetTicketDetails(ticket.TicketId);
            }
            else
            {
                //TODO: Add some error handling here
                return null;
            }
        }
    }
}
