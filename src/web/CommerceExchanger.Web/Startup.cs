using CommerceExchanger.Core.Arithmetic;
using CommerceExchanger.Core.Model;
using CommerceExchanger.Core.Services;
using CommerceExchanger.Core.Services.Base;
using CommerceExchanger.Core.Services.Implementations;
using CommerceExchanger.Integrations.RateProviders.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommerceExchanger.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<RoundCalculator>();
            services.AddSingleton<ExchangeRetriever>();
            services.AddSingleton<IExchangeRateStorage, InMemoryExchangeRateStorage>();
            services.AddSingleton<IExchangeRateProvider>(serviceCollection =>
            {
                var rateProvider =
                    new BaseCurrencyExchangeRateProvider(
                        Currency.USD, serviceCollection.GetRequiredService<ExchangeRetriever>(),
                        serviceCollection.GetRequiredService<RoundCalculator>());
                return rateProvider;
            });
            services.AddSingleton<IExchanger, Exchanger>();
            services.AddHttpClient();
            services.AddRateProviders();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}