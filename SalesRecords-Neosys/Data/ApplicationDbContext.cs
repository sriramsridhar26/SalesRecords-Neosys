using Microsoft.EntityFrameworkCore;
using SalesRecords_Neosys.Model;

namespace SalesRecords_Neosys.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<SalesRecord> SalesRecords { get; set; } 
    }
}
