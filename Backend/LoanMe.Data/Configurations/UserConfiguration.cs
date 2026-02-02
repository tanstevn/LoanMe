using LoanMe.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanMe.Data.Configurations {
    public class UserConfiguration : BaseConfiguration<User> {
        public override void Configure(EntityTypeBuilder<User> builder) {
            base.Configure(builder);

            builder.HasIndex(user => user.FirstName);
            builder.HasIndex(user => user.LastName);
            builder.HasIndex(user => user.DateOfBirth);

            builder.HasOne(user => user.DraftLoan)
                .WithOne(loan => loan.User);
        }
    }
}
