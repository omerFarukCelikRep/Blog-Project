using BlogApp.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Core.DataAccess.Base.EntityFramework.Mapping;
public abstract class BaseEntityMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Status).IsRequired(true);

        builder.Property(x => x.CreatedBy).HasMaxLength(128).IsRequired(true);
        builder.Property(x => x.CreatedDate).IsRequired(true);
        builder.Property(x => x.ModifiedBy).HasMaxLength(128).IsRequired(false);
        builder.Property(x => x.ModifiedDate).IsRequired(false);
    }
}
