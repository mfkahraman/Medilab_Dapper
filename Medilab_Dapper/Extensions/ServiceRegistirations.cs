using Medilab_Dapper.Context;
using Medilab_Dapper.Repositories.DepartmentRepository;

namespace Medilab_Dapper.Extensions
{
    public static class ServiceRegistirations
    {

        public static IServiceCollection AddRepositoriesExt(this IServiceCollection services)
        {
            services.AddScoped<DapperContext>();

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            return services;
        }
    }
}
