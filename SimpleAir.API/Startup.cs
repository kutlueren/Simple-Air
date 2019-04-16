using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using SimpleAir.API.Middleware;
using SimpleAir.Bootstrapper;
using SimpleAir.Domain.Service.Interface;
using SimpleAir.Domain.Service.Mapping;
using SimpleAir.Domain.Service.Model.Mapping;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace SimpleAir.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            //var elasticUri = Configuration["ElasticConfiguration:Uri"];
            //var username = Configuration["ElasticConfiguration:UserName"];
            //var password = Configuration["ElasticConfiguration:Password"];

            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.FromLogContext()
            //    .WriteTo.Async(a => a.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
            //    {
            //        ModifyConnectionSettings = cfg => cfg.BasicAuthentication(username, password),
            //        AutoRegisterTemplate = true,
            //    }))
            //.CreateLogger();

            AppSettingsProvider.Configuration = Configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Mapper.Initialize(cfg => { cfg.AddProfile<FlightProfile>();
                cfg.AddProfile<AirportProfile>();
            });

            services.AddAutoMapper();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            BootStrapper bootStrapper = new BootStrapper();

            bootStrapper.Register(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Linkit Air Api", Version = "v1" });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            loggerFactory.AddSerilog();

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseCors(builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Swagger");
            });

            var flightService = serviceProvider.GetService<IFlightService>();

            flightService.GenerateDummyFlightDataAsync();

            app.UseCustomExceptionHandler("SimpleAir", "API", "/Error/Error");

            app.UseMvc();
        }
    }
}
