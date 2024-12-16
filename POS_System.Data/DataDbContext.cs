using Microsoft.EntityFrameworkCore;
using POS_System.Data.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Data
{
    public class DataDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
            
        }

    }
}
