using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public CreateUserCommandHandler(DevFreelaDbContext dbContext, IAuthService authService, IUserRepository userRepository )
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var passwordHash = _authService.ComputeSha256Hash(request.Password);

                var user = new User(request.FullName, request.Email, request.BirthDate, passwordHash, request.Role);
                await _userRepository.AddAsync(user);
                

                return user.Id;
            }
            catch (Exception ex)    
            {
                throw ex;
            }
        }
    }
}
