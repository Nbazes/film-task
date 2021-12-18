using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FilmApi.Models;
using FilmApi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FilmApi
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
               services.AddSingleton<List<Movie>>(new List<Movie>());
               services.AddScoped<IRepository<Movie>, MovieRepository>();

               services.AddControllers().AddJsonOptions(opts =>
                {
                     opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                     opts.JsonSerializerOptions.IgnoreNullValues = true;
                });


               services.AddSwaggerGen(c =>
               {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmApi", Version = "v1" });
               });


          }

          // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
          public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
          {
               if (env.IsDevelopment())
               {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FilmApi v1"));
               }

               app.UseHttpsRedirection();

               app.UseRouting();

               app.UseAuthorization();

               app.UseEndpoints(endpoints =>
               {
                    endpoints.MapControllers();
               });
          }
     }
}
