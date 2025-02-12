using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppIOC.Class
{
    public class IOCMode
    {
        /// <summary>
        /// 以类的方式注册服务
        /// </summary>
        public static void Test1()
        {
            //以下以服务定位器的方式实现IOC

            //1.创建一个存放服务的集合
            ServiceCollection services = new ServiceCollection();
            //2.添加服务 注册

            //瞬态服务
            //services.AddTransient<TestSerivceImp1>();
            //单例服务
            //services.AddSingleton<TestSerivceImp1>();
            //范围服务
            services.AddScoped<TestSerivceImp1>();


            //此处的ServiceProvider就是服务定位器
            using (ServiceProvider sp = services.BuildServiceProvider())
            {
                //3.向服务定位器要一个服务，就可以拿到对象了
                //TestSerivceImp1 t = sp.GetService<TestSerivceImp1>();
                //t.Name = "Test1";
                //t.SayHi();

                //TestSerivceImp1 t2 = sp.GetService<TestSerivceImp1>();
                ////t.Name = "Test2";
                //t.SayHi();
                //两个对象是否同一对象，是则返回True
                //Console.WriteLine(object.ReferenceEquals(t1, t2));

                //不能在作用域外部引用作用域内部继承了IDisposable的对象，因为在离开作用域后容器会自动调用DisPose方法
                TestSerivceImp1 tt;
                //因为IServiceScope继承IDisposable接口，要用Using释放资源，不然容易内存泄露
                using (IServiceScope scope1 = sp.CreateScope())
                {
                    //在scope中获取Scope相关对象，要在scope.ServiceProvider获取而不是sp
                    TestSerivceImp1 t3 = scope1.ServiceProvider.GetService<TestSerivceImp1>();
                    t3.Name = "Test3";
                    t3.SayHi();

                    TestSerivceImp1 t4 = scope1.ServiceProvider.GetService<TestSerivceImp1>();
                    Console.WriteLine(object.ReferenceEquals(t3, t4));
                    tt = t3;
                }

                using (IServiceScope scope2 = sp.CreateScope())
                {
                    //在scope中获取Scope相关对象，要在scope.ServiceProvider获取而不是sp
                    TestSerivceImp1 t3 = scope2.ServiceProvider.GetService<TestSerivceImp1>();
                    t3.Name = "Test3";
                    t3.SayHi();

                    TestSerivceImp1 t4 = scope2.ServiceProvider.GetService<TestSerivceImp1>();
                    //同一个范围内的是同一个对象
                    Console.WriteLine(object.ReferenceEquals(t3, t4));

                    //结果可知，这个范围和上个范围不是同一个对象
                    Console.WriteLine(object.ReferenceEquals(tt, t4));
                }
            }

        }

        /// <summary>
        /// 以接口的方式注册服务
        /// 这里可以看出来，写业务的人可以不管类的具体实现，直接通过接口要服务（对象）就可以的，而写架构的人注册时放入合适的服务（对象）
        /// </summary>
        public static void Test2()
        {
            ServiceCollection services = new ServiceCollection();
            //服务为ITestService，而TestSerivceImp1是它的实现类
            //注册有很多重载方法，以下为几个例子
            //services.AddScoped(typeof(ITestService),typeof(TestSerivceImp1));
            services.AddScoped<ITestService, TestSerivceImp1>();
            services.AddScoped<ITestService, TestSerivceImp2>();
            using (ServiceProvider sp = services.BuildServiceProvider())
            {
                //注意：注册时用的什么类型，GetService只能传什么类型，比如注册时服务类型传接口，获取服务时就用接口，注册时服务类型传类，获取服务时也要用对应的类
                //注意：如果GetService找不到服务返回null
                ITestService service = sp.GetService<ITestService>();
                //ITestService service = (ITestService)sp.GetService(typeof(ITestService));//反射时使用
                //注意：当使用GetRequiredService方法找不到服务时，此方法会抛异常，而不会返回null Required：必须的
                //ITestService service =sp.GetRequiredService<ITestService>();
                service.Name = "Test22";
                service.SayHi();

                //获取尽可能多的满足条件的服务
                //注意：如果注册了多个相同接口，不同实现类的服务，当使用GetService时获得最后一个注册的服务
                IEnumerable<ITestService> testServices = sp.GetServices<ITestService>();
                foreach (ITestService testService in testServices)
                {
                    Console.WriteLine($"{testService.GetType()}");
                }
                
            }
        }


    }
}
