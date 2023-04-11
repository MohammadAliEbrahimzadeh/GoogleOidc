using GoogleOidcTest.Models;

namespace GoogleOidcTest.Repositories;

public interface IUserRepository
{
        Task<User> LoadByUsernameAsync(string username, CancellationToken cancellationToken);

        Task AddUserAsync(User user, CancellationToken cancellationToken);

        Task CommitAsync(CancellationToken cancellationToken);
}
