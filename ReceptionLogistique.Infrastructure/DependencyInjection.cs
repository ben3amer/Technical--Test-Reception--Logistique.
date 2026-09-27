using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReceptionLogistique.Application.Interfaces;
using ReceptionLogistique.Infrastructure.Persistence;
using ReceptionLogistique.Infrastructure.Repositories;

namespace ReceptionLogistique.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReceptionLogistiqueDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDeliveryRepository, DeliveryRepository>();

            return services;
        }
    }
}