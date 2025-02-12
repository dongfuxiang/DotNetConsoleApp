using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDI.依赖会传染
{
    class TestService
    {

        public void Test()
        {
            //此处是架构注册服务
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<Controller>();
            services.AddScoped<ILog, LogImp1>();
            //services.AddScoped<IConfig, ConfigImp1>();
            //DI：降低模块之间的耦合，让系统更加灵活
            //当有不同实现类时，只需修改注册时的实现类，业务逻辑层不需要修改
            services.AddScoped<IConfig, DBConfigImp1>();
            services.AddScoped<IStorge, StorgeImp1>();

            
            using (var sp = services.BuildServiceProvider())
            {
                var c = sp.GetService<Controller>();
                c.Test();
            }
        }


    }





    /// <summary>
    /// 此处是业务存调用接口实现业务逻辑
    /// </summary>
    class Controller
    {

        private readonly ILog log;
        private readonly IStorge storge;
        //业务不用关心接口具体是怎么实现的,因为框架会从构造函数注入实现类
        public Controller(ILog log, IStorge storge)
        {
            this.log = log;
            this.storge = storge;
        }

        public void Test()
        {
            log.Log("开始上传");
            storge.Save("Test1", "Test2");
            log.Log("上传完毕");
        }
    }

    interface ILog
    {
        public void Log(string message);
    }

    interface IConfig
    {
        public string GetValue(string name);
    }

    interface IStorge
    {
        public void Save(string content, string name);
    }

    public class LogImp1 : ILog
    {
        void ILog.Log(string message)
        {
            Console.WriteLine($"日志：{message}");
        }
    }

    public class ConfigImp1 : IConfig
    {
        public string GetValue(string name)
        {
            return "Hello";
        }
    }
    public class DBConfigImp1 : IConfig
    {
        public string GetValue(string name)
        {
            Console.WriteLine("从数据库读的配置");
            return "DB";
        }
    }
    class StorgeImp1 : IStorge
    {
        //因为此服务是在构造函数中赋值，不允许其在其他地方赋值
        private readonly IConfig config;

        public StorgeImp1(IConfig config)
        {
            this.config = config;
        }

        public void Save(string content, string name)
        {
            string server = config.GetValue(name);
            Console.WriteLine($"服务器{server}的文件名为{name}上传{content}");
        }
    }
}
