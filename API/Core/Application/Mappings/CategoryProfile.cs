using API.Core.Application.Dto;
using API.Core.Domain;
using AutoMapper;

namespace API.Core.Application.Mappings
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            this.CreateMap<Category,CategoryListDto>().ReverseMap();
        }
    }
}
