using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanMe.Data.Extensions {
    public static class EntityTypeBuilderExtensions {
        public static EntityTypeBuilder<TEntity> ConfigureId<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class {
            builder.HasKey("Id");

            builder.Property("Id")
                .ValueGeneratedOnAdd();

            return builder;
        }
    }
}
