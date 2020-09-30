using System;
using System.Net.Http;
using CommerceExchanger.Core.Services;
using CommerceExchanger.Integrations.RateProviders.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CommerceExchanger.Integrations.RateProviders.Extensions
{
    public static class RateProvidersExtensions
    {
        public static void AddRateProviders(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient(RateHttpClientName(typeof(EcbRateProvider)),
                client => { client.BaseAddress = new Uri("https://sdw-wsrest.ecb.europa.eu/service/data/EXR/"); });
            serviceCollection.AddHttpClient(RateHttpClientName(typeof(NbrbRateProvider)),
                client => { client.BaseAddress = new Uri("https://www.nbrb.by/api/exrates/rates"); });
            serviceCollection.AddSingleton<IExternalRateProvider, EcbRateProvider>();
            serviceCollection.AddSingleton<IExternalRateProvider, NbrbRateProvider>();
        }

        public static HttpClient GetRateHttpClient(this IHttpClientFactory factory, Type providerType)
        {
            return factory.CreateClient(RateHttpClientName(providerType));
        }

        public static string RateHttpClientName(Type providerType)
        {
            return $"{providerType.Name}-HttpClient";
        }
    }
}