using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogServices
{
    public interface ILogProvider
    {
        void LogInfo(string msg);
        void LogError(string msg);
    }
}
