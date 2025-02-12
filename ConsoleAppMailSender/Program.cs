using ConfigService;
using LogServices;
using MailServices;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppMailSender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<IConfigService, EnvVarConfigService>();
            services.AddScoped<ILogProvider, ConsoleLogProvider>();
            services.AddScoped<IMailService, MailService>();

            using (var sp=services.BuildServiceProvider())
            {
                //第一个跟对象只能使用ServiceLocator（服务定位器）
                IMailService mailService = sp.GetRequiredService<IMailService>();
                mailService.Send("邮件1","小明","内容");

            }
        }
    }
}
