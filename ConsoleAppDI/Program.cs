using ConsoleAppDI.依赖会传染;

namespace ConsoleAppDI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestService testService = new TestService();
            testService.Test();
            Console.ReadLine();
        }
    }
}
