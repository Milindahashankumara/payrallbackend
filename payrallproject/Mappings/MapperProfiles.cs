using AutoMapper;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Mappings
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Employe, EmployeDto>().ReverseMap();
            CreateMap<OT, OTDto>().ReverseMap();
        }
    }
    
}
