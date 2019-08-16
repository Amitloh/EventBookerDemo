using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventBookerAndroid.Core.Models;

namespace EventBookerAndroid.Core.Interfaces
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetTickets(int eventId);
        Task<Ticket> GetTicketDetails(int ticketId);

        //Dummy method for now for saving a sold ticket
        int SaveTicket(Ticket ticket);
    }
}
