using Domain.Chat.Interfaces;
using Domain.Chat.Models;
using Infrastructure.Extensions;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(environment.ContentRootPath)
                            .AddJsonFile("appsettings.json", false, true)
                            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true);

            Configuration = configuration;
            Environment = environment;

            Configuration = builder.Build(); 
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddChatService(Configuration);
            services.AddScoped<IBotChatRepository>(hc => 
                    new BotChatRepository("/stockquote", 
                    new System.Net.Http.HttpClient { BaseAddress = new Uri("https://localhost:7058/") }));

            services.AddControllersWithViews();
            
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                                        builder =>
                                        {
                                            builder.WithOrigins("https://localhost:44359")
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()
                                                    .SetIsOriginAllowed((host) => true)
                                                    .AllowCredentials();
                                        }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.ConfigureChatHub(Configuration);
            });
        }
    }
}
