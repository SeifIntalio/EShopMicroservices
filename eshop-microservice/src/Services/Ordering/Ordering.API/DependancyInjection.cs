using Carter;

namespace Ordering.API;

public static class DependancyInjection
{

    // before building the application

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCarter();
        return services;
    }

    // after building the application
    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        return app;
    }
}
