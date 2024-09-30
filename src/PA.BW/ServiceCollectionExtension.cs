using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PA.BW.Reporter;
using PA.Contracts.Interfaces;
using PL.Contracts.Settings;
using PL.Events;

namespace PA.BW;

public static class ServiceCollectionExtension
{
    private const string _defaultRabbitMqSectionName = "RabbitSettings";
    public static IServiceCollection AddBWServices(this IServiceCollection services)
    {
        services.AddTransient<IPovorotEngineReporter, PovorotReporter>();
        return services;
    }
    
    public static IServiceCollection ConfigureMasstransit(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(cfg =>
        {
            cfg.AddMasstransitRabbitMq(configuration);
        });
        return services;
    }

    public static void AddMasstransitRabbitMq(this IBusRegistrationConfigurator configurator,
        IConfiguration configuration,
        Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? configure = null
    )
    {
        var rabbitSettings = configuration.GetSection(_defaultRabbitMqSectionName ?? _defaultRabbitMqSectionName)
                                 ?.Get<RabbitSettings>() ??
                             throw new Exception(
                                 $"Не заданы настройки для RabbitMq (раздел \"{_defaultRabbitMqSectionName ?? _defaultRabbitMqSectionName}\").");
        configurator.UsingRabbitMq((context, config) =>
        {
            config.Host(rabbitSettings.Uri, rabbitSettings.Port, rabbitSettings.VirtualHost,
                delegate(IRabbitMqHostConfigurator h)
                {
                    h.Username(rabbitSettings.Username);
                    h.Password(rabbitSettings.Password);
                });

            configure?.Invoke(context, config);
            config.UseInMemoryOutbox(context);
            config.ConfigureEndpoints(context);
        });
    }
    
}