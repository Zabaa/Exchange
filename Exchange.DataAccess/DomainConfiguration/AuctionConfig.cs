using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Domain.Auction;

namespace Exchange.DataAccess.DomainConfiguration
{
    public class AuctionConfig : EntityTypeConfiguration<Auction>
    {
        public AuctionConfig()
        {
            ToTable("Auction");
            HasKey(s => s.Id).Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(s => s.Name).HasColumnType("nvarchar").HasMaxLength(32).IsRequired();
            Property(s => s.Description).HasColumnType("nvarchar").HasMaxLength(512).IsOptional();
            Property(s => s.OpenPrice).HasColumnType("numeric").IsRequired();
            Property(s => s.Price).HasColumnType("numeric").IsOptional();
            Property(s => s.StartDate).HasColumnType("datetime").IsRequired();
            Property(s => s.PredictedEndDate).HasColumnType("datetime").IsRequired();
            Property(s => s.LastPriceChangeDate).HasColumnType("datetime").IsOptional();
            Property(s => s.UserId).HasColumnType("nvarchar").IsRequired();
        }
    }
}
