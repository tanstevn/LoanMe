using LoanMe.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanMe.Data.Configurations {
    public class ProductConfiguration : BaseConfiguration<Product> {
        public override void Configure(EntityTypeBuilder<Product> builder) {
            base.Configure(builder);

            builder.Property(product => product.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(product => product.Description)
                .HasMaxLength(512);

            builder.HasData(new() {
                Id = 1,
                Name = "House",
                Description = "This product provides loan that is interest-free.",
                LoanTermMinimum = 1,
                LoanTermMaximum = 300
            }, new() {
                Id = 2,
                Name = "Car",
                Description = "This product gives the first 2 months interest-free but the loan term duration is 6 months minimum.",
                LoanTermMinimum = 6,
                LoanTermMaximum = 60
            }, new() {
                Id = 3,
                Name = "Phone",
                Description = "This product provides no interest-free.",
                LoanTermMinimum = 1,
                LoanTermMaximum = 3
            });
        }
    }
}
