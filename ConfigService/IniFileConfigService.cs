using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigService
{
    public class IniFileConfigService : IConfigService
    {
        /// <summary>
        /// 文件的路径
        /// </summary>
        public string FilePath { get; set; }
        public string GetValue(string name)
        {

            var kv = File.ReadAllLines(FilePath).Select(s => s.Split('='))
                .Select(str => new { Name = str[0], Value = str[1] })
                .FirstOrDefault(kv => name == kv.Name);

            
            if(kv!=null)
            {
                return kv.Value;
            }
            else
            {
                return null;
            }
        }
    }
}
