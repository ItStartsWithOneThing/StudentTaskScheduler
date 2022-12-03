using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StudentTaskScheduler.BL.Authorization;
using StudentTaskScheduler.BL.Automapper.Profiles;
using StudentTaskScheduler.BL.HashService;
using StudentTaskScheduler.BL.Options;
using StudentTaskScheduler.BL.Services.AuthorizationService;
using StudentTaskScheduler.BL.Services.JobsService;
using StudentTaskScheduler.BL.Services.StudentsService;
using StudentTaskScheduler.DAL;
using StudentTaskScheduler.DAL.Repositories;
using System.Collections.Generic;
using System.Reflection;

namespace StudentTaskScheduler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();         
            services.AddScoped<ITokenGenerator, TokenGenerator>();         
            services.AddScoped<IHashService, HashService>();

            services.Configure<AuthorizationOptions>(options =>
                Configuration.GetSection(nameof(AuthorizationOptions)).Bind(options));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentTaskScheduler", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(JobProfile))
            };
            services.AddAutoMapper(assemblies);

            services.AddDbContext<EfCoreDbContext>(options =>
                        options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));


            services.AddScoped(typeof(IDbGenericRepository<>), typeof(DbGenericRepository<>));
            services.AddScoped<IDbJobRepository, DbJobRepository>();

            
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentTaskScheduler v1"));
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
