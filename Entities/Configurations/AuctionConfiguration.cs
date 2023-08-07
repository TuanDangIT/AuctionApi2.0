using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApi.Entities.Configurations
{
    public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.HasOne(a => a.Category).WithMany(c => c.Auctions).HasForeignKey(a => a.CategoryId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(a => a.User).WithMany(u => u.Auctions).HasForeignKey(a => a.UserId);
            builder.Property(a => a.Description).HasColumnType("varchar(50)");
            builder.Property(a => a.Price).HasColumnType("decimal(10,2)");
            builder.Property(a => a.CreatedDate).HasDefaultValueSql("getutcdate()");
            builder.Property(a => a.UpgradeDate).ValueGeneratedOnUpdate();
        }
    }
}
