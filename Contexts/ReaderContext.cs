using Beadandó.Shared;
using Microsoft.EntityFrameworkCore;
using System;

namespace Beadandó.Contexts;

public class ReaderContext : DbContext
{
    public ReaderContext(DbContextOptions<ReaderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reader> Readers { get; set; }

}