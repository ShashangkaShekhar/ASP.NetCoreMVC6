using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreMVC.Models
{
    public partial class TicketBookingContext : DbContext
    {
        public TicketBookingContext(DbContextOptions<TicketBookingContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.Property(e => e.DestinationFrom).HasMaxLength(50);

                entity.Property(e => e.DestinationTo).HasMaxLength(50);

                entity.Property(e => e.TicketDate).HasColumnType("datetime");

                entity.Property(e => e.TicketFee).HasColumnType("numeric");

                entity.Property(e => e.Vat)
                    .HasColumnName("VAT")
                    .HasColumnType("numeric");
            });
        }

        public virtual DbSet<Ticket> Ticket { get; set; }
    }
}