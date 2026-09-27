using ERP.Application.Helpers;
using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Application.Servicies;
using ERP.Infreastructure.Helper;
using ERP.Infreastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ERP.Infreastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDBConnectionFactory, DbConnectionFactory>();
            services.AddScoped<IStoredProcedtureExecutor, StoredProceduredExecutor>();
            services.AddScoped<IPeopleRepository, PeopleRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRolePermissionsRepository, RolePermissionsRepository>();
            services.AddScoped<ICategoriesRepository,CategoriesRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IWarehousesRepository, WarehousesRepository>();
            services.AddScoped<IProductWarehousesRepository, ProductWarehousesRepository>();
            services.AddScoped<IPurchaseInvoicesRepository, PurchaseInvoicesRepository>();
            services.AddScoped<IPurchaseInvoiceItemsRepository, PurchaseInvoiceItemsRepository>();
            services.AddScoped<ISaleInvoicesRepository, SaleInvoicesRepository>();
            services.AddScoped<ISaleInvoiceItemsRepository, SaleInvoiceItemsRepository>();
            services.AddScoped<ISaleReturnsRepository, SaleReturnsRepository>();
            services.AddScoped<ISaleReturnItemsRepository, SaleReturnItemsRepository>();
            services.AddScoped<IStockMovementsRepository, StockMovementsRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogsRepository>();
            services.AddScoped<ISettingsRepository, SettingsRepository>();
            return services;
        }
    }
}
