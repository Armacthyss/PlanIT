using PlanIT.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;


namespace PlanIT.Models.Maps
{
    public class tbl_events_maps : EntityTypeConfiguration<tbl_events_model>
    {
        public tbl_events_maps()
        {
         
            HasKey(i => i.EventID);

           
            Property(i => i.EventID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(i => i.CreatedAt).HasColumnName("CreatedAt");
            ToTable("tbl_events");
        }
    }
}