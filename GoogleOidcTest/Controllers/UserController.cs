using GoogleOidcTest.DTOs;
using GoogleOidcTest.Models;
using GoogleOidcTest.Services;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace GoogleOidcTest.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class UserController : ControllerBase
        {
                private readonly IUserService? _userService;
                private readonly IAuthenticationSchemeProvider _schemeProvider;

                private const string LoginProviderKey = "LoginProvider";
                private const string XsrfKey = "XsrfId";

                public UserController(IUserService userService, IAuthenticationSchemeProvider schemeProvider)
                {
                        _userService = userService;
                        _schemeProvider = schemeProvider;
                }

                [HttpPost]
                [Route("Register")]
                public async Task<Response> Register(RegisterDto registerDto, CancellationToken cancellationToken)
                {
                        return await _userService!.RegisterAsync(registerDto, cancellationToken);
                }

                [HttpPost]
                [Route("Login")]
                public async Task<Response> Login(LoginDto loginDto, CancellationToken cancellationToken)
                {
                        return await _userService!.LoginAsync(loginDto, cancellationToken);
                }

                [HttpPost]
                [Route("Google-Login")]
                public IActionResult Login()
                {
                        var properties = new AuthenticationProperties
                        {
                                RedirectUri = "https://localhost:7184/api/user/Test"
                        };

                        var result = Challenge(properties, GoogleDefaults.AuthenticationScheme);

                        return result;
                }

                [Authorize]
                [HttpGet]
                [Route("Test")]
                public async Task<IActionResult> Test()
                {
                        var cliams = User.Claims.ToList();

                        var token = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
                        var idToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
                        var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

                        return Ok();
                }


        }
}
