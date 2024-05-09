using Microsoft.EntityFrameworkCore;
using Models;

namespace Contexts;

public class PostgreSqlContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserModel> Users { get; set; }
}
