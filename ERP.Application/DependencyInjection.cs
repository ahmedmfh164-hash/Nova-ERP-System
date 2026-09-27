using ERP.Application.Helpers;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Services;
using ERP.Application.Interfaces.Servicies;
using ERP.Application.Services;
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
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IWarehousesService, WarehousesService>();
            services.AddScoped<IProductWarehousesService, ProductWarehousesService>();
            services.AddScoped<IPurchaseInvoicesService, PurchaseInvoicesService>();
            services.AddScoped<IPurchaseInvoiceItemsService, PurchaseInvoiceItemsService>();
            services.AddScoped<ISaleInvoicesService, SaleInvoicesService>();
            services.AddScoped<ISaleInvoiceItemsService, SaleInvoiceItemsService>();
            services.AddScoped<ISaleReturnsService, SaleReturnsService>();
            services.AddScoped<ISaleReturnItemsService, SaleReturnItemsService>();
            services.AddScoped<IStockMovementsService, StockMovementsService>();
            services.AddScoped<IAuditLogService, AuditLogService>();
            services.AddScoped<ISettingsService, SettingsService>();
            return services;
        }
    }
}