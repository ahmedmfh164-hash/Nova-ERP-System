using ERP.Application.Helpers;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Servicies;
using ERP.Application.Servicies;
using Microsoft.Extensions.DependencyInjection;

namespace ERP.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddScoped<IDirectoryPathService, DirectoryPathService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRolePermissionsService, RolePermissionsService>();
            services.AddScoped<ICategoriesService,CategoriesService>();
            return services;
        }
    }
}