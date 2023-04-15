using FluentValidation;
using FluentValidation.AspNetCore;
using GoogleOidcTest.Context;
using GoogleOidcTest.Models;
using GoogleOidcTest.Repositories;
using GoogleOidcTest.Services;
using GoogleOidcTest.Validations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static System.Net.WebRequestMethods;

namespace GoogleOidcTest;

internal static class DependencyInjectionExtension
{

        internal static IServiceCollection InjectDBContext(this IServiceCollection services, IConfiguration configuration) =>
                services.AddDbContext<GoogleOidcTestDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("GoogleOidcTestDbContext")));

        internal static IServiceCollection InjectServices(this IServiceCollection services) =>
                services.AddScoped<IUserService, UserService>();

        internal static IServiceCollection InjectRepositories(this IServiceCollection services) =>
                services.AddScoped<IUserRepository, UserRepository>();

        internal static IServiceCollection InjectAuthentication(this IServiceCollection services) =>
               services.AddAuthentication(options =>
                 {
                         options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                         options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                 })
                 .AddCookie(options =>
                        {
                                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                        })
                .AddGoogle(options =>
                        {
                                options.ClientId = "314460019879-lvkocu1110mckhrc7sre4uo95g0q720a.apps.googleusercontent.com";
                                options.ClientSecret = "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";
                                options.SaveTokens = true;
                                options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
                        }).Services;
        
        internal static IServiceCollection InjectFluentValidation(this IServiceCollection services) =>
                services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<RegisterValidation>();

        internal static IServiceCollection InjectCors(this IServiceCollection services) =>
              services.AddCors(p => p.AddPolicy("GoogleOidc", builder =>
                {
                        builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
                }));


        internal static IServiceCollection InjectHttpContextAccessor(this IServiceCollection services) =>
                services.AddHttpContextAccessor();

}
