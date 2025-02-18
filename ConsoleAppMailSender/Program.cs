using ConfigService;
using MailServices;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppMailSender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            //FUNC委托
            //services.AddScoped(typeof(IConfigService), s => new IniFileConfigService() { FilePath = "mail.ini" });
            //services.AddScoped<IConfigService, EnvVarConfigService>();
            //services.AddScoped<ILogProvider, ConsoleLogProvider>();
            services.AddIniFileConfig("mail.ini");
            services.AddScoped<IConfigService, EnvVarConfigService>();
           
            services.AddLayeredConfig();
            services.AddConsloeLog();//使用扩展方法，只用添加Microsoft.Extensions.DependencyInjection的引用
            services.AddScoped<IMailService, MailService>();

            using (var sp = services.BuildServiceProvider())
            {
                //第一个跟对象只能使用ServiceLocator（服务定位器）
                IMailService mailService = sp.GetRequiredService<IMailService>();
                mailService.Send("邮件1", "小明", "内容");

            }
        }
    }
}
