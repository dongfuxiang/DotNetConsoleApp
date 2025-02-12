using ConfigService;
using LogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailServices
{
   public class MailService : IMailService
    {
        private readonly ILogProvider log;
        private readonly IConfigService config;

        //IOC容器注入
        public MailService(ILogProvider log, IConfigService config)
        {
            this.log = log;
            this.config = config;
        }
        public void Send(string title, string to, string body)
        {
            log.LogInfo("准备发送邮件");
            string smtpServer = config.GetValue("SmtpServer");
            string userName = config.GetValue("UserName");
            string password = config.GetValue("Password");
            log.LogInfo($"邮件服务器地址：{smtpServer},{userName},{password}");
            Console.WriteLine($"发送邮件！{title},{to}");
            log.LogInfo("邮件发送完成");
        }
    }
}
