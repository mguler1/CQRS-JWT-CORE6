using API.Core.Application.Features.CQRS.Commands;
using API.Core.Application.Interfaces;
using API.Core.Domain;
using AutoMapper;
using MediatR;

namespace API.Core.Application.Features.CQRS.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async  Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var updateEntity =await _repository.GetByIdAsync(request.Id);
            if (updateEntity != null)
            {
                updateEntity.CategoryId = request.CategoryId;
                updateEntity.Stock = request.Stock;
                updateEntity.Price = request.Price;
                updateEntity.Name = request.Name;
                await _repository.UpdateAsync(updateEntity);
            }
            return Unit.Value;
          
        }
    }
}
