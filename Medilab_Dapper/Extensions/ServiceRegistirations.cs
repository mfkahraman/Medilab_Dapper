using Medilab_Dapper.Context;
using Medilab_Dapper.Repositories.DepartmentRepository;
using Medilab_Dapper.Repositories.DoctorRepository;
using Medilab_Dapper.Repositories.ImageRepository;

namespace Medilab_Dapper.Extensions
{
    public static class ServiceRegistirations
    {

        public static IServiceCollection AddRepositoriesExt(this IServiceCollection services)
        {
            services.AddScoped<DapperContext>();

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IImageService, ImageService>();

            return services;
        }
    }
}
