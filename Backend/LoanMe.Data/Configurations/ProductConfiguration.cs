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

            builder.Property(product => product.LoanTermMinimum)
                .IsRequired();

            builder.Property(product => product.LoanTermMaximum)
                .IsRequired();

            builder.Property(product => product.LoanAmountMinimum)
                .IsRequired();

            builder.Property(product => product.LoanAmountMaximum)
                .IsRequired();

            builder.Property(product => product.InterestRate)
                .IsRequired();

            builder.Property(product => product.EstablishmentFee)
                .IsRequired();

            builder.HasData(new() {
                Id = 1,
                Name = "House",
                Description = "This product is interest-free loan.",
                LoanTermMinimum = 1,
                LoanTermMaximum = 300,
                LoanAmountMinimum = 10000m,
                LoanAmountMaximum = 2000000m,
                InterestRate = default,
                EstablishmentFee = 300m,
                InterestFreeMonths = null
            }, new() {
                Id = 2,
                Name = "Car",
                Description = "This product gives the first 2 months interest-free but the loan term duration is 6 months minimum.",
                LoanTermMinimum = 6,
                LoanTermMaximum = 60,
                LoanAmountMinimum = 1000m,
                LoanAmountMaximum = 20000m,
                InterestRate = 0.05m,
                EstablishmentFee = 250m,
                InterestFreeMonths = 2
            }, new() {
                Id = 3,
                Name = "Phone",
                Description = "This product provides no interest-free.",
                LoanTermMinimum = 1,
                LoanTermMaximum = 3,
                LoanAmountMinimum = 100m,
                LoanAmountMaximum = 2000m,
                InterestRate = 0.02m,
                EstablishmentFee = 100m,
                InterestFreeMonths = 0
            });
        }
    }
}
