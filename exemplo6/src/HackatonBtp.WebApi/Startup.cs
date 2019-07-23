using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using HackatonBtp.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using HackatonBtp.Domain.Interfaces.Repository;
using HackatonBtp.Data.Repository;
using HackatonBtp.WebApi.Filters;
using FluentValidation.AspNetCore;
using HackatonBtp.Application.Email;
using HackatonBtp.Domain.Models;
using HackathonBtp.Application.Email;

namespace HackatonBtp.WebApi
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc(opt => 
            {
                opt.Filters.Add(typeof(ValidatorActionFilter));
                opt.Filters.Add(new SqlExceptionFilter());

            }).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddTransient<ITimeRepository, TimeRepository>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IContantoEmailSender, ContatoSender>();

            services.Configure<EmailOptions>(Configuration.GetSection("EmailConfigs"));

            services.AddHttpContextAccessor();

            AutoMapperConfig.RegisterMappings();

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseDefaultFiles();
            app.UseStaticFiles();

        }
    }
}