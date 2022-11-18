using BD.Entities;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class AspNetContext : DbContext
{
    public virtual DbSet<Folder> Folder { get; set; }

    public AspNetContext(DbContextOptions<AspNetContext> options) : base(options) { }
}