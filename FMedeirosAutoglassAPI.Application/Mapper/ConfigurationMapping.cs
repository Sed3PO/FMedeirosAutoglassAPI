using AutoMapper;
using FMedeirosAutoglassAPI.Application.DTO;
using FMedeirosAutoglassAPI.Domain.Entity;

namespace FMedeirosAutoglassAPI.Application.Mapper
{
    public class ConfigurationMapping : Profile
    {
        public ConfigurationMapping()
        {
            CreateMap<Product, ProductDTO>()
                .ReverseMap();
        }
    }
}