using API.Core.Application.Dto;
using API.Core.Application.Features.CQRS.Queries;
using API.Core.Application.Interfaces;
using API.Core.Domain;
using AutoMapper;
using MediatR;

namespace API.Core.Application.Features.CQRS.Handlers
{
    public class GetCategoryQueryHandler:IRequestHandler<GetCategoryQueryRequest,CategoryListDto>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public async  Task<CategoryListDto> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByFilterAsync(x => x.Id == request.Id);
            return _mapper.Map<CategoryListDto>(data);
        }
    }
}
