using Beadandó.Shared;
using Microsoft.EntityFrameworkCore;
using System;

namespace Beadandó.Contexts;

public class LoanContext : DbContext
{
    public LoanContext(DbContextOptions<LoanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Loan> Loans { get; set; }
    public virtual DbSet<Item> LoanItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Reader)
            .WithMany()
            .HasForeignKey(l => l.ReaderId);

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany()
            .HasForeignKey(l => l.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
