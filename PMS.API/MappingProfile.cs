using AutoMapper;
using PMS.DTOs;
using PMS.Entities;

namespace PMS.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, AddProductDTO>();
        CreateMap<AddProductDTO, Product>();

        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>();
    }
}
