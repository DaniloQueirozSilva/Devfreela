using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly DevFreelaDbContext _dbContext;
        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(User user)
        {

            await _dbContext.Users.AddAsync(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return result!;
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash)
        {
            var result = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password == passwordHash);

            return result!;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
