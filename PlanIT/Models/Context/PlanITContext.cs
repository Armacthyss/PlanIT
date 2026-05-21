using PlanIT.Models.Maps;
using PlanIT.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace PlanIT.Models.Context
{
    public class PlanITContext : DbContext
    {
        public PlanITContext() : base("Name=planitdb") { }


        public DbSet<tbl_events_model> tbl_events { get; set; }
        public virtual DbSet<tbl_users_model> tbl_users { get; set; }
        public DbSet<tbl_bookings_model> tbl_bookings { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Configurations.Add(new tbl_users_maps());
            modelBuilder.Configurations.Add(new tbl_events_maps());
            modelBuilder.Configurations.Add(new tbl_bookings_maps());
        }
    }
}