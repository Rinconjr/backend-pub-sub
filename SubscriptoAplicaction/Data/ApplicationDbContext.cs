using Microsoft.EntityFrameworkCore;
using SubscriptoAplicacion.Models;

namespace SubscriptoAplicacion.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<MessageModel> Messages { get; set; }
    }
}
