using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Data.Extensions
{
    public static class AddDataExtension
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddDbContext<DataDbContext>(options =>
                options.UseNpgsql("xxxx"));

            return services;
        }
    }
}
