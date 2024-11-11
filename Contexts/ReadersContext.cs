using Microsoft.EntityFrameworkCore;
using System;

namespace Beadandó.Contexts;

public class ReadersContext : DbContext
{
    public ReadersContext(DbContextOptions<ReadersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reader> Readers { get; set; }

}