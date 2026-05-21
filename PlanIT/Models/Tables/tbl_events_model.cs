using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanIT.Models.Tables
{
    public class tbl_events_model
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int TotalTickets { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Price { get; set; }
    }
}