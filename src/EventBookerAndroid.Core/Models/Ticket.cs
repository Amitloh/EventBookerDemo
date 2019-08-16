using System;
using System.Collections.Generic;
using System.Text;

namespace EventBookerAndroid.Core.Models
{
    public class Ticket : BaseModel
    {
        public int TicketId { get; set; }
        public int EventId { get; set; }
        public string TicketHolderName { get; set; }
    }
}
