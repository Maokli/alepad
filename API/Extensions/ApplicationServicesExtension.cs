using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
         IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<DataContext>(opt => {
                opt.UseSqlite(config
                    .GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}