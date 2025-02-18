using LogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//因为是IServiceCollection的扩展方法，方便用户直接.出来，所以使用此命名空间，给微软自带的类扩展方法
namespace Microsoft.Extensions.DependencyInjection
{

    public static class ConsoleLogExtensions
    {
        /// <summary>
        /// 扩展方法，调用这个方法直接注册
        /// </summary>
        /// <param name="services"></param>
        public static void AddConsloeLog(this IServiceCollection services)
        {
            services.AddScoped<ILogProvider, ConsoleLogProvider>();
        }
    }
}
