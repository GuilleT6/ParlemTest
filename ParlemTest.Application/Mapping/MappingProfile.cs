using AutoMapper;
using MongoDB.Bson;
using ParlemTest.Domain.DTOs;
using ParlemTest.Domain.Entities;

namespace ParlemTest.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.GivenName} {src.FamilyName1}"));

            CreateMap<Customer, GetCustomerDto>()
               .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.GivenName} {src.FamilyName1}"));

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<ProductDto, Product>()
                .ReverseMap();

            CreateMap<GetProductDto, Product>()
               .ReverseMap();

            CreateMap<CreateCustomerDto, Customer>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.Products, opt => opt.MapFrom(src => new List<Product>()));


            CreateMap<UpdateCustomerDto, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Products, opt => opt.Ignore());

            CreateMap<CreateProductDto, Product>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ObjectId.GenerateNewId()));

            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
