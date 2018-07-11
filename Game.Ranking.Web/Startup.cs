using AutoMapper;
using Game.Ranking.Infrastructure.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            // Dependency Injection
            Game.Ranking.Model.Startup.ConfigureServices(services);
            Game.Ranking.Infrastructure.Startup.ConfigureServices(services);
            Game.Ranking.Services.Startup.ConfigureServices(services);

            // ElasticSearch connection configuration
            services.AddSingleton<ElasticClientConfigurator>(provider =>
            {
                return new ElasticClientConfigurator(new Nest.ConnectionSettings(new Uri(Configuration.GetConnectionString("ElasticSearch"))));
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
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Game Ranking API V1");
            });

            app.UseMvc();
        }
    }
}
