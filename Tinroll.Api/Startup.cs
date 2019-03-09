﻿using System;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Tinroll.Business.Managers;
using Tinroll.Business.Managers.Interfaces;
using Tinroll.Data;
using Tinroll.Data.Repositories;
using Tinroll.Data.Repositories.Interfaces;

namespace Tinroll.Api
{
    public class Startup
    {
        private IHostingEnvironment _appHost;

        public Startup(IConfiguration configuration, IHostingEnvironment appHost)
        {
            Configuration = configuration;
            _appHost = appHost;
        }

        readonly string AllowedCorsOriginPolicy = "_allowedCorsOriginPolicy";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var mvcCoreBuilder = services.AddMvcCore();

            mvcCoreBuilder
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddCors()
                .AddApiExplorer();

            services.AddCors(o => o.AddPolicy(AllowedCorsOriginPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddDbContext<TinContext>
                (options => options.UseSqlite($"Filename={_appHost.ContentRootPath}/tin.db"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Tinroll API", Version = "v1" });
            });

            //dependency injection
            
            //managers
            services.AddScoped<IQuestionManager, QuestionManager>();
            services.AddScoped<IAnswerManager, AnswerManager>();
            services.AddScoped<IUserManager, UserManager>();

            //repositories
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
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
            app.UseCors(AllowedCorsOriginPolicy); 
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tinroll API v1");
                c.RoutePrefix = string.Empty;
            });


        }
    }
}
