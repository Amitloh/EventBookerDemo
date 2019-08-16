using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBookerAndroid.Core.Interfaces;
using EventBookerAndroid.Core.Models;

namespace EventBookerAndroid.Core.Repositories
{
    public class TicketRepository : BaseRepository, ITicketRepository
    {
        //TODO: temporary list of tickets for quick testing. REMOVE
        private static readonly List<Ticket> AllTickets = new List<Ticket>()
        {
            new Ticket
            {
                TicketId = 1,
                TicketHolderName = "Travis Keel",
                EventId = 1
            }
        };

        public async Task<Ticket> GetTicketDetails(int ticketId)
        {
            return await Task.FromResult(AllTickets.FirstOrDefault(t => t.TicketId == ticketId));
        }

        public async Task<IEnumerable<Ticket>> GetTickets(int eventId)
        {
            return await Task.FromResult(AllTickets.Where(t => t.EventId == eventId));
        }

        //TODO: Update once real data persistence working
        public int SaveTicket(Ticket ticket)
        {
            AllTickets.Add(ticket);
            return 1;
        }
    }
}
