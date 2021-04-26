using Alpha.Web.API.Data;
using Alpha.Web.API.Data.Entities;
using Alpha.Web.API.Data.Repositories;
using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Domain.Models.Validators;
using Alpha.Web.API.Security.Helpers;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Alpha.Web.API.Domain.Services
{
    /// <summary>
    /// This class injects all dependencies needed to the app run, so that the StartUp class will keep clean
    /// </summary>
    public static class DependenciesInjector
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection _services, IConfiguration configuration)
        {
            //Set database configurations
            _services.AddDbContext<AlphaDbContext>(config =>
                config.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            //Sevices injection
            _services.AddScoped<IAspNetUsersService, AspNetUsersService>();
            _services.AddScoped<IMessagesService, MessagesService>();
            _services.AddScoped<IAuditLogsService, AuditLogsService>();
            _services.AddScoped<IAccountService, AccountService>();

            //Repositories injection
            _services.AddScoped<IAspNetUsersRepository, AspNetUsersRepository>();
            _services.AddScoped<IMessagesRepository, MessagesRepository>();
            _services.AddScoped<IAuditLogsRepository, AuditLogsRepository>();

            //ModelValidators injection
            _services.AddScoped<IValidator<UserModel>, UserModelValidator>();
            _services.AddScoped<IValidator<MessageModel>, MessageModelValidator>();

            //Add IdentityMagager
            _services.AddIdentity<AspNetUser, AspNetRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequireDigit = false;
                config.Password.RequiredUniqueChars = 0;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<AlphaDbContext>();

            //Add authentication by jwt
            _services.AddAuthentication()
            .AddCookie()
            .AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Tokens:Issuer"],
                    ValidAudience = configuration["Tokens:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]))
                };
            });

            return _services;
        }
    }
}
