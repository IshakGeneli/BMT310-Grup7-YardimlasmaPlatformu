using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.Business.Concrete;
using xHelp.DataAccess.Abstract;
using xHelp.DataAccess.Concrete.EntityFrameworkCore;

namespace xHelp.API
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
