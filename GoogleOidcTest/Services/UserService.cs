using GoogleOidcTest.DTOs;
using GoogleOidcTest.Helpers;
using GoogleOidcTest.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace GoogleOidcTest.Services;

public class UserService : IUserService
{
        private readonly IUserRepository? _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserService(IUserRepository? userRepository, IHttpContextAccessor contextAccessor)
        {
                _userRepository = userRepository;
                _contextAccessor = contextAccessor;
        }

        public async Task<Response> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken)
        {
                var user = await _userRepository!.LoadByUsernameAsync(registerDto!.Username!, cancellationToken);

                if (user is not null)
                        return new Response { IsSuccess = false };

                await _userRepository.AddUserAsync(new Models.User
                {
                        Id = Guid.NewGuid().ToString(),
                        Password = registerDto!.Password!.GenerateHash(),
                        Username = registerDto.Username
                }
                , cancellationToken);

                await _userRepository.CommitAsync(cancellationToken);

                return new Response { IsSuccess = true };

        }

        public async Task<Response> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken)
        {
                var user = await _userRepository!.LoadByUsernameAsync(loginDto!.Username!, cancellationToken);

                if (user is null || user!.Password != loginDto.Password!.GenerateHash())
                        return new Response { IsSuccess = false };

                List<Claim> claims = new List<Claim>()
                             {
                              new Claim(ClaimTypes.NameIdentifier ,user!.Id!.ToString()),
                             };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties()
                {
                        IsPersistent = loginDto.RememberMe
                };

                await _contextAccessor.HttpContext!.SignInAsync(principal, properties);


                return new Response { IsSuccess = true };

        }
}
