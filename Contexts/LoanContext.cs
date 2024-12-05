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

}
