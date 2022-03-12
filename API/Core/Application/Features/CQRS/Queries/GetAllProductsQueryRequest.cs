using API.Core.Application.Dto;
using MediatR;

namespace API.Core.Application.Features.CQRS.Queries
{
    public class GetAllProductsQueryRequest:IRequest<List<ProductListDto>>
    {

    }
}
