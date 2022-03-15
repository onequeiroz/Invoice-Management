using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vibbraneo.API.Business;
using Vibbraneo.API.Business.Interfaces;
using Vibbraneo.API.Repository;
using Vibbraneo.API.Repository.Interfaces;

namespace Vibbraneo.API
{
    public class DependencyInjection
    {
        public static void Configurar(IConfiguration configuration, IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IReportsRepository, ReportsRepository>();
            #endregion

            #region Business
            services.AddScoped<ICategoryBusiness, CategoryBusiness>();
            services.AddScoped<ICompanyBusiness, CompanyBusiness>();
            services.AddScoped<IExpenseBusiness, ExpenseBusiness>();
            services.AddScoped<IInvoiceBusiness, InvoiceBusiness>();
            services.AddScoped<IReportsBusiness, ReportsBusiness>();
            #endregion

            #region Factories
            services.AddTransient<IDatabaseConnectionFactory>((connectionFactory) =>
            {
                string connectionString = configuration.GetConnectionString("sqlServerConnectionString");

                return new DatabaseConnectionFactory(connectionString);
            });
            #endregion
        }
    }
}
