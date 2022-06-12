using AutoMapper;
using SalesRecords_Neosys.DTO;
using SalesRecords_Neosys.Model;

namespace SalesRecords_Neosys.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<SalesRecord, SalesRecordDTO>().ReverseMap();
        }
    }
}
