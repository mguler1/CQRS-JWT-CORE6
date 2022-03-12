using API.Core.Application.Dto;
using API.Core.Domain;
using AutoMapper;

namespace API.Core.Application.Mappings
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            this.CreateMap<Product, ProductListDto>().ReverseMap();
        }
    }
}
