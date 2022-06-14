using HotelService.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PlayerService.MessageHandlers;
using Shared;
using HotelService.Repository;
using HotelService.Services;
using HotelService.Configuration;
using System;
using HotelService.Authentication;

namespace HotelService
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
            string connectionString = Environment.GetEnvironmentVariable("DbConnectionString");
            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseMySQL(connectionString));
           
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelService", Version = "v1" });
            });
            services.AddMessagePublishing("HotelService", builder =>
            {
                builder.WithHandler<NewBookingMessageHandler>("NewBooking");
            });
            services.AddDataProtection();

            services.AddScoped<AdminService>()
                .AddScoped<CipherService>()
                .AddScoped<IAws3Services, Aws3Services>();

            var amazonS3Section = Configuration.GetSection(nameof(AppConfiguration));
            var amazonS3Settings = amazonS3Section.Get<AppConfiguration>();
            services.AddSingleton<IAppConfiguration>(amazonS3Settings);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelService v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
     
    }
}
