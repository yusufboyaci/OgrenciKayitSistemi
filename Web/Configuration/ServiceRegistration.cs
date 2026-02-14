using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.Concrete;

namespace Web.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // PostgreSQL DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Service Registrations
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<ITeacherService, TeacherService>();

        return services;
    }
}
