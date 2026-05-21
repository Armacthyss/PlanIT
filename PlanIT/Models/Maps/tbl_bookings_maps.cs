using PlanIT.Models.Tables;
using System.Data.Entity.ModelConfiguration;

namespace PlanIT.Models.Maps
{
    public class tbl_bookings_maps : EntityTypeConfiguration<tbl_bookings_model>
    {
        public tbl_bookings_maps()
        {

            this.ToTable("tbl_bookings");

    
            this.HasKey(t => t.BookingID);

          
            this.Property(t => t.BookingID).HasColumnName("BookingID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.EventID).HasColumnName("EventID");
            this.Property(t => t.TicketCount).HasColumnName("TicketCount");
            this.Property(t => t.BookingDate).HasColumnName("BookingDate");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}