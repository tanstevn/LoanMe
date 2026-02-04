using LoanMe.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanMe.Data.Configurations {
    public class ActiveLoanConfiguration : BaseConfiguration<ActiveLoan> {
        public override void Configure(EntityTypeBuilder<ActiveLoan> builder) {
            base.Configure(builder);

            builder.Property(activeLoan => activeLoan.ApplicationNumber)
                .IsRequired();

            builder.Property(activeLoan => activeLoan.LoanAmount)
                .IsRequired();

            builder.Property(activeLoan => activeLoan.LoanTerm)
                .IsRequired();

            builder.Property(activeLoan => activeLoan.RepaymentAmount)
                .IsRequired();

            builder.Property(activeLoan => activeLoan.TotalInterest)
                .IsRequired();

            builder.HasOne(activeLoan => activeLoan.User)
                .WithOne(user => user.ActiveLoan);

            builder.HasOne(activeLoan => activeLoan.Product)
                .WithMany(product => product.ActiveLoans)
                .HasForeignKey(activeLoan => activeLoan.ProductId);

            builder.Navigation(activeLoan => activeLoan.User)
                .AutoInclude();

            builder.Navigation(activeLoan => activeLoan.Product)
                .AutoInclude();
        }
    }
}
