using GoogleOidcTest.DTOs;

namespace GoogleOidcTest.Services;

public interface IUserService
{
        Task<Response> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken);

        Task<Response> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken);
}
