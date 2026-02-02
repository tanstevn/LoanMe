using LoanMe.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanMe.Data.Configurations {
    public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder) {
            builder.ConfigureId();
        }
    }
}
