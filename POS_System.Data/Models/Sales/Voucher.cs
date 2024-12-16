using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Data.Models.Sales
{
    public class Voucher
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal? DiscountProcentage { get; set; }
        public decimal? DiscountPrice { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
