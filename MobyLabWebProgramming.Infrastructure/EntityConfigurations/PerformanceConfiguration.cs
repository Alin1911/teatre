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
    public class PerformanceConfiguration : IEntityTypeConfiguration<Performance>
    {
        public void Configure(EntityTypeBuilder<Performance> builder)
        {
            builder.Property(e => e.Id).IsRequired();
            builder.HasKey(x => x.Id);
            builder.Property(e => e.IdPiesa).IsRequired();
            builder.Property(e => e.StartDate).IsRequired();
            builder.Property(e => e.EndDate).IsRequired();
            builder.Property(e => e.HallId).IsRequired();
            builder.Property(e => e.Price).IsRequired();
            builder.Property(e => e.TicketMaxScan).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.UpdatedAt).IsRequired();
            builder.HasOne<Piece>().WithMany().HasForeignKey(p => p.IdPiesa);
            builder.HasOne<Hall>().WithMany().HasForeignKey(p => p.HallId);
        }
    }
}
