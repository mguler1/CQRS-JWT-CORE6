using API.Core.Application.Dto;
using MediatR;

namespace API.Core.Application.Features.CQRS.Queries
{
    public class GetCategoriesQueryRequest:IRequest<List<CategoryListDto>>
    {
    }
}
