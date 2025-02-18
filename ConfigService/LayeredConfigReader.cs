using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigService
{
    public class LayeredConfigReader : IConfigReader
    {
        private readonly IEnumerable<IConfigService> services;
        //构造函数注入
        public LayeredConfigReader(IEnumerable<IConfigService> services)
        {
            this.services = services;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetValue(string name)
        {
            string value = null;
            foreach (var service in services)
            {
                string newValue = service.GetValue(name);
                if (newValue != null)
                {
                    value = newValue;//最后一个不为null的值就是最终值
                }

            }
            return value;

        }
    }
}
