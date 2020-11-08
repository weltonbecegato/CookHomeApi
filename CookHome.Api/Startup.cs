using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookHome.Api.Configuracao;
using CookHome.Api.Dominio;
using CookHome.Api.Servicos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CookHome.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cook Home", Version = "v1" });
            });

            services.AddCors(o => o.AddPolicy("LocalOrigins", builder => {
                builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyMethod();
            }));
            services.AddDbContext<CookHomeContext>(options =>
                options.UseSqlServer(Configuration.GetValue<string>("ConexaoBancoDados")));
            this.ConfigurarInjecoes(services);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.DocExpansion(DocExpansion.None);
            });
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("LocalOrigins");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigurarInjecoes(IServiceCollection services)
        {
            var google = new GoogleMapsConfiguracao();
            new ConfigureFromConfigurationOptions<GoogleMapsConfiguracao>(Configuration.GetSection("GoogleMaps")).Configure(google);
            services.AddSingleton(google);

            services.AddHttpClient<IGoogleMapsServico, GoogleMapsServico>();
            services.AddSingleton<IGoogleMapsServico, GoogleMapsServico>();
        }
    }
}
