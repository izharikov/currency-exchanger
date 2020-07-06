using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CommerceExchanger.Core.Arithmetic;
using CommerceExchanger.Core.Exceptions;
using CommerceExchanger.Core.Model;
using CommerceExchanger.Core.Services;
using CommerceExchanger.Core.Services.Base;
using CommerceExchanger.Core.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            services.AddSingleton<IExchangeRateStorage>((_) =>
            {
                var inMemoryStorage = new InMemoryExchangeRateStorage();
                inMemoryStorage.Add(new ExchangeRateRequest(Currency.EUR, Currency.EUR), 1);
                inMemoryStorage.Add(new ExchangeRateRequest(Currency.BYN, Currency.EUR), 0.37M);
                inMemoryStorage.Add(new ExchangeRateRequest(Currency.USD, Currency.EUR), 0.89M);
                return inMemoryStorage;
            });
            services.AddSingleton<IExchangeRateProvider>(serviceCollection =>
            {
                var rateProvider =
                    new BaseCurrencyExchangeRateProvider(
                        Currency.EUR, serviceCollection.GetRequiredService<IExchangeRateStorage>(),
                        serviceCollection.GetRequiredService<RoundCalculator>());
                return rateProvider;
            });
            services.AddSingleton<IExchanger, Exchanger>();
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