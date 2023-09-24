using Microsoft.EntityFrameworkCore;

namespace NoNameLib.Domain.Tests.SQLite;

public class SQLiteContext : DbContext
{
    public SQLiteContext(
        string connectionString) : base(new DbContextOptionsBuilder().UseSqlite(connectionString).Options) { }

    public DbSet<TestDomain> Domains { get; set; }
}
