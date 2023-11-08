using JwtAuth.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuth.Data
{
    public class JWTDbContext : DbContext
    {
        public JWTDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
