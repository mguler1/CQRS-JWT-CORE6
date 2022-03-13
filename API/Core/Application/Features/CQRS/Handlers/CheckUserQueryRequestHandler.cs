using API.Core.Application.Dto;
using API.Core.Application.Features.CQRS.Queries;
using API.Core.Application.Interfaces;
using API.Core.Domain;
using MediatR;

namespace API.Core.Application.Features.CQRS.Handlers
{
    public class CheckUserQueryRequestHandler : IRequestHandler<CheckUserQueryRequest, CheckUserResponseDto>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<AppRole> _roleRepository;

        public CheckUserQueryRequestHandler(IRepository<AppUser> userRepository, IRepository<AppRole> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async  Task<CheckUserResponseDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
        {
            var dto = new CheckUserResponseDto();
            var user=await _userRepository.GetByFilterAsync(x=>x.UserName==request.UserName && x.Password==request.Password);
            if (user==null)
            {
                dto.IsExist = false;
            }
            else
            {
                dto.UserName = user.UserName;
                dto.RoleId = user.AppRoleId;
                dto.IsExist = true;
                var role = await _roleRepository.GetByFilterAsync(x => x.Id == user.AppRoleId);
                dto.Role = role.Definition;
            }
            return dto;
        }
    }
}
