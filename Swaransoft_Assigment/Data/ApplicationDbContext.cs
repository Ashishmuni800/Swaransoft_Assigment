using Microsoft.EntityFrameworkCore;
using Swaransoft_Assigment.Models;

namespace Swaransoft_Assigment.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }

        public DbSet<UserDetails> UserDetails { get; set; }

        public DbSet<State> State { get; set; }      
        public DbSet<stateCity> StateCity { get; set; }         
    }
}
