using System;
using System.Collections.Generic;
using System.Text;

namespace EventBookerAndroid.Core.Models
{
    public class Event: BaseModel
    {
        //To be used as primary key
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDateTime { get; set; }
        public int TotalTickets { get; set; }
        public int AvailableTickets { get; set; }

        //TODO: Add event photo (use BitMap to store/load?)
    }
}
