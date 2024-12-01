using BeadandóShared;
using Microsoft.EntityFrameworkCore;
using System;

namespace Beadandó.Contexts;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions<BookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Item> Items { get; set; }
}
