using API.Core.Application.Enums;
using API.Core.Application.Features.CQRS.Commands;
using API.Core.Application.Interfaces;
using API.Core.Domain;
using MediatR;

namespace API.Core.Application.Features.CQRS.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest>
    {
        private readonly IRepository<AppUser> _repository;

        public RegisterUserCommandHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async  Task<Unit> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new AppUser
            {
                AppRoleId = (int)RoleType.Member,
                Password= request.Password,
                UserName=request.UserName,
            });
            return Unit.Value;
        }
    }
}
