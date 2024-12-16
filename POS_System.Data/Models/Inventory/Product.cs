using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Data.Models.Inventory
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
    }
}
