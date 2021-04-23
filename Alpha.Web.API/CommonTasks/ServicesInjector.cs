using Alpha.Web.API.Data;
using Alpha.Web.API.Data.Entities;
using Alpha.Web.API.Data.Repositories;
using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Domain.Models.Validators;
using Alpha.Web.API.Security.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Web.API.Domain.Services
{
    public static class DependenciesInjector
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection _services, IConfiguration configuration)
        {
            //Set database configurations
            _services.AddDbContext<AlphaDbContext>(config =>
                config.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            //Sevices injection
            _services.AddScoped<IUsersService, UsersService>();
            _services.AddScoped<IMessagesService, MessagesService>();
            _services.AddScoped<IAuditLogsService, AuditLogsService>();

            //Repositories injection
            _services.AddScoped<IUsersRepository, UsersRepository>();
            _services.AddScoped<IMessagesRepository, MessagesRepository>();
            _services.AddScoped<IAuditLogsRepository, AuditLogsRepository>();

            //ModelValidators injection
            _services.AddScoped<IValidator<UserModel>, UserModelValidator>();
            _services.AddScoped<IValidator<MessageModel>, MessageModelValidator>();

            //Add IdentityMagager
            _services.AddIdentity<User, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequireDigit = false;
                config.Password.RequiredUniqueChars = 0;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<AlphaDbContext>();

            //Inject helpers
            _services.AddScoped<IUserHelper, UserHelper>();

            return _services;
        }
    }
}
