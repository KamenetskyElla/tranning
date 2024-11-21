using System.Reflection;

namespace OrdersDemo.Mediator;


public static class MediatorExtensions
{
    public static IServiceCollection AddExtendedMediatR(this IServiceCollection services, params Type[] handlerAssemblyMarkerTypes)
    {
        var assemblies = handlerAssemblyMarkerTypes.Select(x => x.Assembly).ToArray();

        return AddExtendedMediatR(services, assemblies);
    }

    public static IServiceCollection AddExtendedMediatR(this IServiceCollection services, params Assembly[] assemblies)
    {

        services.AddMediatR(options =>
        {
            options.Lifetime = ServiceLifetime.Scoped;
            options.RegisterServicesFromAssemblies(assemblies);
        });
        return services;
    }
}
