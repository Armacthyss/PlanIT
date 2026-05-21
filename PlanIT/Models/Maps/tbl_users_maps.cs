using PlanIT.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema; 
using System.Data.Entity.ModelConfiguration;

namespace PlanIT.Models.Maps
{
    public class tbl_users_maps : EntityTypeConfiguration<tbl_users_model>
    {
        public tbl_users_maps()
        {
            HasKey(i => i.UserID);

            
            Property(i => i.UserID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

          
            Property(i => i.UpdatedAt).HasColumnName("UpdateAt");

            ToTable("tbl_users");
        }
    }
}