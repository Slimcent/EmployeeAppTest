using EmployeeWeb.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWeb.Middlewares
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
           services.AddDbContext<EmployeeContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("EmployeeConnection"), b =>
       b.MigrationsAssembly("Employee.Web")));
    }
}
