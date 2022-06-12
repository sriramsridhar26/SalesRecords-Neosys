using SalesRecords_Neosys.Model;

namespace SalesRecords_Neosys.DTO
{
    public class SalesResponseDTO
    {
        public List<SalesRecord> salesRecords { get; set; }
        public int Pages { get; set; } 
        public int CurrentPage { get; set; }
    }
}
