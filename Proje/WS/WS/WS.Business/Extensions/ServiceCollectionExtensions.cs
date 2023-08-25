using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Business.Implementations;
using WS.Business.Interfaces;
using WS.DataAccess.Implementations.EF.Repositories;
using WS.DataAccess.Interfaces;

namespace WS.Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DepartmanBs));
         
            services.AddScoped<IDepartmanBs, DepartmanBs>();
            services.AddScoped<IDepartmanRepository, DepartmanRepository>();
           
            services.AddScoped<IEmployeeBs, EmployeeBs>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IClientBs, ClientBs>();
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<IExpenseBs, ExpenseBs>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();

            services.AddScoped<IOrderBs, OrderBs>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IPaymentBs, PaymentBs>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddScoped<IProductBs, ProductBs>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProjectBs, ProjectBs>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.AddScoped<IProjectBs, ProjectBs>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
           
            services.AddScoped<ISupplierBs, SupplierBs>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
           
            services.AddScoped<IAdminPanelUserBs, AdminPanelUserBs>();
            services.AddScoped<IAdminPanelUserRepository, AdminPanelUserRepository>();




        }
    }
}
