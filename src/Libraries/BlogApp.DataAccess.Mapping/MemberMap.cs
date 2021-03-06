using BlogApp.Core.DataAccess.Base.EntityFramework.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mapping;
public class MemberMap : AuditableEntityMap<Member>
{
    public override void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(256).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Biography).HasMaxLength(512).IsRequired(false);
        builder.Property(x => x.ProfilePicture).IsRequired(false);
        builder.Property(x => x.Url).HasMaxLength(512).IsRequired(false);
        builder.Property(x => x.IdentityId).IsRequired();

        base.Configure(builder);
    }
}
