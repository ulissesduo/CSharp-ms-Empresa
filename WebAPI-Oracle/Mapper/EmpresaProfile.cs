using AutoMapper;
using WebAPI_Oracle.Dto;
using WebAPI_Oracle.Entity;

namespace WebAPI_Oracle.Mapper
{
    public class EmpresaProfile : Profile
    {
        public EmpresaProfile()
        {
            CreateMap<Empresa, EmpresaRequestDTO>().ReverseMap();
            CreateMap<Empresa, EmpresaResponseDTO>();

        }
    }
}
