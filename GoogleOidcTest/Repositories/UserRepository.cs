using GoogleOidcTest.Context;
using GoogleOidcTest.Models;
using Microsoft.EntityFrameworkCore;

namespace GoogleOidcTest.Repositories;

public class UserRepository : IUserRepository
{
        private readonly GoogleOidcTestDbContext? _dbContext;

        public UserRepository(GoogleOidcTestDbContext? dbContext)
        {
                _dbContext = dbContext;
        }

        public async Task<User?> LoadByUsernameAsync(string username, CancellationToken cancellationToken) =>
                 await _dbContext!.Users!.SingleOrDefaultAsync(u => u.Username == username, cancellationToken);

        public async Task AddUserAsync(User user, CancellationToken cancellationToken) =>
                await _dbContext!.Users!.AddAsync(user, cancellationToken);

        public async Task CommitAsync(CancellationToken cancellationToken) =>
           await _dbContext!.SaveChangesAsync(cancellationToken);
}
