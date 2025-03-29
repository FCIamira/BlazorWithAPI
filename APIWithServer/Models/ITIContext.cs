using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace day1.Models
{
    public class ITIContext : IdentityDbContext<AppUser>
    {
        public ITIContext(DbContextOptions<ITIContext> options):base(options) { }
        public DbSet<Category> categories {  get; set; }
        public DbSet<Product> products { get; set; }
    }
}
