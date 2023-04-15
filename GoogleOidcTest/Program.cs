using GoogleOidcTest;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
        options.AddSecurityDefinition("HarmalleSaz", new OpenApiSecurityScheme
        {
                Type = SecuritySchemeType.OpenIdConnect,
                OpenIdConnectUrl = new Uri("https://accounts.google.com/.well-known/openid-configuration")
        });
});




#region Injections

builder.Services
        .InjectDBContext(builder.Configuration)
        .InjectServices()
        .InjectRepositories()
        .InjectAuthentication()
        .InjectFluentValidation()
        //.InjectCors()
        .InjectHttpContextAccessor();

#endregion

var app = builder.Build();
//app.UseCors("GoogleOidc");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
        app.UseSwagger(options =>
        {
        });
        app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
