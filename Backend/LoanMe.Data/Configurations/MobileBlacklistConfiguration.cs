using LoanMe.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanMe.Data.Configurations {
    public class MobileBlacklistConfiguration : BaseConfiguration<BlacklistMobile> {
        public override void Configure(EntityTypeBuilder<BlacklistMobile> builder) {
            base.Configure(builder);

            builder.Property(mobileList => mobileList.MobileNumber)
                .IsRequired()
                .HasMaxLength(64);

            builder.HasIndex(mobileList => mobileList.MobileNumber);

            builder.HasData(new() {
                Id = 1,
                MobileNumber = "1234567890",
                CreatedAt = new DateTime()
            }, new() {
                Id = 2,
                MobileNumber = "0987654321",
                CreatedAt = new DateTime()
            });
        }
    }
}
