using AutoMapper;
using Game.Ranking.Infrastructure.Replication.Impl;
using Game.Ranking.Services.Interfaces;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace Game.Ranking.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            // MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            // Dependency Injection
            Game.Ranking.Model.Startup.ConfigureServices(services);
            Game.Ranking.Infrastructure.Replication.Startup.ConfigureServices(services);
            Game.Ranking.Infrastructure.InMemory.Startup.ConfigureServices(services);
            Game.Ranking.Services.Startup.ConfigureServices(services);

            // ElasticSearch connection configuration
            services.AddSingleton<ElasticClientConfigurator>(provider =>
            {
                var settings = new Nest.ConnectionSettings(new Uri(Configuration.GetConnectionString("ElasticSearch")));
                settings.DisableDirectStreaming();
                return new ElasticClientConfigurator(settings);
            });

            // Initialize AutoMapper
            Mapper.Initialize(cfg =>
            {
                Game.Ranking.Services.Startup.AddMapperProfiles(cfg);
            });

            // Add Swagger
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new Info { Title = "Game.Ranking.API", Version = "v1" });
                
                foreach (var xml in Directory.EnumerateFiles(AppContext.BaseDirectory, "Game.Ranking.*.xml"))
                    cfg.IncludeXmlComments(xml, true);
            });

            // Add Hangfire (Job scheduler)
            services.AddHangfire(config => config.UseStorage(new MemoryStorage()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Dev Exception Page
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Game Ranking API V1");
            });

            // Hangfire
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            // Schedule Replication Service
            RecurringJob.AddOrUpdate(() => app.ApplicationServices.GetRequiredService<IGameResultService>().Replicate(), Cron.MinuteInterval(5));

            // MVC
            app.UseMvc();
        }
    }
}
