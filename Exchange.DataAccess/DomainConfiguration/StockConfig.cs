using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Exchange.Domain;

namespace Exchange.DataAccess.DomainConfiguration
{
    public class StockConfig : EntityTypeConfiguration<Stock>
    {
        public StockConfig()
        {
            ToTable("Stock");
            HasKey(s => s.Id).Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(s => s.Symbol).HasColumnType("varchar").HasMaxLength(32).IsRequired();
            Property(s => s.Price).HasColumnType("decimal(7,2)").IsRequired();
            Property(s => s.DayOpen).HasColumnType("decimal(7,2)").IsOptional();
            Property(s => s.LastChangeDate).HasColumnType("datetime").IsOptional();
        }
    }
}
