using Alpha.Web.API.Data.Repositories;
using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Domain.Models.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Web.API.Domain.Services
{
    public static class DependenciesInjector
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection _services)
        {
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

            return _services;
        }
    }
}
