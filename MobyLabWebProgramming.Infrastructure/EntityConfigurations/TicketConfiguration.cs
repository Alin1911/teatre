using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(e => e.Id).IsRequired();
            builder.HasKey(x => x.Id);
            builder.Property(e => e.PerformanceId).IsRequired();
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.NumarScanari).IsRequired();
            builder.Property(e => e.TransactionId).IsRequired();
            builder.Property(e => e.Code).HasMaxLength(255).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.UpdatedAt).IsRequired();
            builder.HasOne<Performance>().WithMany().HasForeignKey(t => t.PerformanceId);
            builder.HasOne<Transaction>().WithMany().HasForeignKey(t => t.TransactionId);
        }
    }
}
