using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(e => e.Id).IsRequired();
            builder.HasKey(x => x.Id);
            builder.Property(e => e.TitluRol).HasMaxLength(255).IsRequired();
            builder.Property(e => e.IdPiesa).IsRequired();
            builder.Property(e => e.IdActor).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.UpdatedAt).IsRequired();
            builder.HasOne<Piece>().WithMany().HasForeignKey(r => r.IdPiesa);
            builder.HasOne<Actor>().WithMany().HasForeignKey(r => r.IdActor);
        }
    }
}
