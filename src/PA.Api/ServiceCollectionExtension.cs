using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PA.Api;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }

    public static WebApplication UseWebApi(this WebApplication app)
    {
        app.UseRouting();
        app.MapControllers();
        return app;
    }
}