using Company.Customer.Domain.Repositories;
using Company.Customer.Persistence.MappingProfiles;
using Company.Customer.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Customer.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("CustomerDb"),
                b => b.MigrationsAssembly("Company.Customer.API")));

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
