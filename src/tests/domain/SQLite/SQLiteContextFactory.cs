using Microsoft.EntityFrameworkCore.Design;

namespace NoNameLib.Domain.Tests.SQLite
{
    public class SQLiteContextFactory : IDesignTimeDbContextFactory<SQLiteContext>
    {
        public SQLiteContext CreateDbContext(string[] args)
        {
            return new SQLiteContext(MainTestingObject.GetConnectionString());
        }
    }
}
