using API.Core.Application.Features.CQRS.Commands;
using API.Core.Application.Interfaces;
using API.Core.Domain;
using AutoMapper;
using MediatR;

namespace API.Core.Application.Features.CQRS.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public   CreateCategoryCommandHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
           await  _repository.CreateAsync(new Category
            {
                Definition = request.Definition
            });
            return Unit.Value;
        }
    }
}
