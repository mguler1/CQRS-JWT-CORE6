using API.Core.Application.Features.CQRS.Commands;
using API.Core.Application.Interfaces;
using API.Core.Domain;
using AutoMapper;
using MediatR;

namespace API.Core.Application.Features.CQRS.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async  Task<Unit> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var updateEntity = await _repository.GetByIdAsync(request.Id);
            if (updateEntity != null)
            {
                updateEntity.Definition = request.Definition;
            
                await _repository.UpdateAsync(updateEntity);
            }
            return Unit.Value;
        }
    }
}
