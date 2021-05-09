using System.Net.Mime;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApplication2.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }//creating a table called Items
        
    }
}