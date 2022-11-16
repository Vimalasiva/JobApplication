using JobApplication.Core.IRepository;
using JobApplication.Core.IService;
using JobApplication.Repository;
using JobApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Utility
{
    public  class DIResolver
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            #region Context accesor
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region Services
            services.AddScoped<IJobService, JobServices>();

            #endregion

            #region Repository
            services.AddScoped<IJobRepository, JobRepository>();
            #endregion
        }
    }
}
