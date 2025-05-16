using AMAPP.API.DTOs.Product;
using AMAPP.API.Models;
using AutoMapper;

namespace AMAPP.API.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            // Map Produto to ProductDto
            CreateMap<Product, ProductDto>();
                //.ForMember(dest => dest.ProductTypeId, opt => opt.MapFrom(src => src.ProductType.Id));

            // Map CreateProductDto to Produto
            CreateMap<CreateProductDto, Product>()
                //.ForMember(dest => dest.ProductType, opt => opt.Ignore()) // TipoProduto is set separately
                .ForMember(dest => dest.Photo, opt => opt.Ignore()) // Photo is set separately
                .ForMember(dest => dest.ProducerInfo, opt => opt.Ignore()); // ProducerInfo is set separately
            
            // Map UpdateProductDto to Produto
            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                //.ForMember(dest => dest.ProductType, opt => opt.Ignore()) // TipoProduto is set separately
                .ForMember(dest => dest.Photo, opt => opt.Ignore()) // Photo is set separately
                .ForMember(dest => dest.ProducerInfo, opt => opt.Ignore()); // ProducerInfo is set separately
        }
    }
}
