using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.Business.Concrete;
using xHelp.DataAccess.Abstract;
using xHelp.DataAccess.Concrete.EntityFrameworkCore;
using xHelp.Entity.Concrete;

namespace xHelp.API
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // data access layer
            services.AddSingleton<IMissionDal, EfMissionDal>();
            services.AddSingleton<IAchievementDal, EfAchievementDal>();
            services.AddSingleton<IContactDal, EfContactDal>();
            services.AddSingleton<IEvidenceDal, EfEvidenceDal>();

            // business layer
            services.AddSingleton<IMissionService, MissionManager>();
            services.AddSingleton<IAchievementService, AchievementManager>();
            services.AddSingleton<IContactService, ContactManager>();
            services.AddSingleton<IEvidenceService, EvidenceManager>();
            services.AddScoped<IUserService, UserManager>();

            // identity
            services.AddDbContext<xHelpDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("IdentityDbContext")));
            services.AddIdentity<User, UserRole>().AddEntityFrameworkStores<xHelpDbContext>().AddDefaultTokenProviders();

            // swagger
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "xHelp API", Version = "v1" }); });

            // cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllCors",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "xHelp API V1"); });

            app.UseCors("AllowAllCors");

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
