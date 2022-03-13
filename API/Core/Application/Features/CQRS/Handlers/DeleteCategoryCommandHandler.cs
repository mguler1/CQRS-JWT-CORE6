using API.Core.Application.Features.CQRS.Commands;
using API.Core.Application.Interfaces;
using API.Core.Domain;
using AutoMapper;
using MediatR;

namespace API.Core.Application.Features.CQRS.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async  Task<Unit> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var deleteEntity = await _repository.GetByIdAsync(request.Id);
            if (deleteEntity != null)
            {
                await _repository.RemoveAsync(deleteEntity);
            }
            return Unit.Value;
        }
    }
}
