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
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.Property(e => e.Id).IsRequired();
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Nume).HasMaxLength(255).IsRequired();
            builder.Property(e => e.Prenume).HasMaxLength(255).IsRequired();
            builder.Property(e => e.DataNastere).IsRequired();
            builder.Property(e => e.Salariu).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.UpdatedAt).IsRequired();
        }
    }
}
