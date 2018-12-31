using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AzureBot.Services.AzureServices;
using AzureBot.Models.Slack;
using AzureBot.Services.Slack;
using AzureBot.Factories;
using AzureBot.Models.AzureModels;

namespace AzureBot
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
            services.Configure<SlackSettings>(Configuration.GetSection("Slack"));
            services.Configure<AzureSettings>(Configuration.GetSection("Azure"));
            services.AddScoped<IHashService, HmacSha256HashService>();
            services.AddScoped<ISignatureValidationService, SlackSignatureValidationService>();
            services.AddScoped<IVirtualMachineService, VirtualMachineService>();
            services.AddScoped<IVirtualMachineCommandFactory, VirtualMachineCommandFactory>();
            services.AddScoped<ICommandParseService, VirtualMachineCommandParseService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
