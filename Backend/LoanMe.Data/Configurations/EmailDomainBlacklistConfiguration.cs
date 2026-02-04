using LoanMe.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanMe.Data.Configurations {
    public class EmailDomainBlacklistConfiguration : BaseConfiguration<BlacklistEmailDomain> {
        public override void Configure(EntityTypeBuilder<BlacklistEmailDomain> builder) {
            base.Configure(builder);

            builder.Property(domainList => domainList.Domain)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(domainList => domainList.Domain);

            var utcNow = DateTime.UtcNow;

            builder.HasData(new() {
                Id = 1,
                Domain = "hacker.com",
                CreatedAt = utcNow
            }, new() {
                Id = 2,
                Domain = "helloworld.com",
                CreatedAt = utcNow
            });
        }
    }
}
