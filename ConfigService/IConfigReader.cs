using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigService
{
    //1.“可覆盖的配置读取器”
    public interface IConfigReader
    {
        /// <summary>
        /// 如果配置找不到就返回null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetValue(string name);

    }
}
