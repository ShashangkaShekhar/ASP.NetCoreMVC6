using System;
using System.Collections.Generic;

namespace CoreMVC.Models
{
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public string DestinationFrom { get; set; }
        public string DestinationTo { get; set; }
        public DateTime? TicketDate { get; set; }
        public decimal? TicketFee { get; set; }
        public decimal? Vat { get; set; }
    }
}
