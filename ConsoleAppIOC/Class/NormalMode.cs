using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppIOC.Class
{
    public class NoramlMode
    {
        public void Test()
        {
            //没有用IOC容器时，创建对象
            TestSerivceImp1 testSerivceImp1 = new TestSerivceImp1();
            testSerivceImp1.Name = "Tom";
            testSerivceImp1.SayHi();
        }
    }




    public interface ITestService
    {
        public string Name { get; set; }
        public void SayHi();
    }

    public class TestSerivceImp1 : ITestService
    {
        public string Name { get; set; }

        public void SayHi()
        {
            Console.WriteLine($"Hi,i am {Name}");
        }
    }

    public class TestSerivceImp2 : ITestService
    {
        public string Name { get; set; }

        public void SayHi()
        {
            Console.WriteLine($"你好,我是 {Name}");
        }
    }
}
