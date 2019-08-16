using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventBookerAndroid.Core.Models;

namespace EventBookerAndroid.Core.Interfaces
{
    public interface ITicketDataService
    {
        Task<IEnumerable<Ticket>> GetTicketsForEvent(int eventId);
        Task<Ticket> SellTicketToEvent(int eventId, string holderName);
    }
}
