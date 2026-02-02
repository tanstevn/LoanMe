using LoanMe.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanMe.Data.Configurations {
    public class DraftLoanConfiguration : BaseConfiguration<DraftLoan> {
        public override void Configure(EntityTypeBuilder<DraftLoan> builder) {
            base.Configure(builder);

            builder.HasOne(draftLoan => draftLoan.User)
                .WithOne(user => user.DraftLoan);

            builder.Navigation(draftLoan => draftLoan.User)
                .AutoInclude();
        }
    }
}
