using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SalesRecords_Neosys.Model
{
    public class SalesRecord
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string ItemTyp { get; set; }

        [Required]
        public string SalesChannel { get; set; }

        [Required]
        [MaxLength(1)]
        public char OrderPriority { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [Range(0,long.MaxValue,ErrorMessage ="Value exceeds Order Id limit!")]
        public long OrderId { get; set; }

        [Required]
        public DateTime ShipDate { get; set; }

        [Required]
        [Range(1,Int32.MaxValue, ErrorMessage ="Value should be greater than 0 and lesser than 2.5 billion")]
        public int UnitSold { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        [Required]
        public double UnitCost { get; set; }

        public double TotalRevenue { get; private set; }

        public double TotalCost { get; private set; }

        public double TotalProfit { get; private set; }

    }
}
