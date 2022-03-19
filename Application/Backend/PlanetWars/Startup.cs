using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PlanetWars.Data.Context;
using PlanetWars.Core.Configuration;
using PlanetWars.Services.ConcreteServices;
using PlanetWars.Services;
using PlanetWars.Communication;
using PlanetWars.DTOs.MappingProfiles;

namespace PlanetWars
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
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IGalaxyService, GalaxyService>();
            services.AddTransient<IPlanetService, PlanetService>();
            services.AddTransient<IPlayerColorService, PlayerColorService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IGameMapService, GameMapService>();

            services.AddAutoMapper(typeof(Mappings));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlanetWars", Version = "v1" });
            });

            services.AddDbContext<PlanetWarsDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PlanetWars"));
            }, ServiceLifetime.Transient);

            services.AddCors(options => {
                options.AddPolicy("CORS", builder =>
                {
                    builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins("http://localhost:4200")
                            .AllowCredentials();
                });
            });

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlanetWars v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CORS");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/GameUpdates");
            });
        }
    }
}
