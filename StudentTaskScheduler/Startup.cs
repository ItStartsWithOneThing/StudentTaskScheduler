using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StudentTaskScheduler.BL.Automapper.Profiles;
using StudentTaskScheduler.BL.Services.JobsService;
using StudentTaskScheduler.BL.Services.StudentsService;
using StudentTaskScheduler.DAL;
using StudentTaskScheduler.DAL.Repositories;
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

            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(JobProfile))
            };
            services.AddAutoMapper(assemblies);

            services.AddDbContext<EfCoreDbContext>(options =>
                        options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));


            services.AddScoped(typeof(IDbGenericRepository<>), typeof(DbGenericRepository<>));
            services.AddScoped<IDbJobRepository, DbJobRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentTaskScheduler", Version = "v1" });
            });
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
