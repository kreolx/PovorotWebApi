using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PA.Contracts.Interfaces;
using PA.GqClient.Managers;

namespace PA.GqClient;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddGqClient(this IServiceCollection services, IConfiguration configuration)
    {
        var povorotBaseAddress = configuration.GetValue<string>("povorotBaseAddress");
        services.AddConferenceClient()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(povorotBaseAddress!));
        services.AddTransient<IPovorotClient, PovorotClient>();
        return services;
    }
}