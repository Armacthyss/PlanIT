using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanIT.Models.Tables
{
    public class tbl_bookings_model
    {
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public int EventID { get; set; }
        public int TicketCount { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
    }
}