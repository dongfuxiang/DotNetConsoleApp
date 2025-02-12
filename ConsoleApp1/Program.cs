namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string filename = AppDomain.CurrentDomain.BaseDirectory + @"1.txt";
            await File.WriteAllTextAsync(filename, "Hello");
            string s = await File.ReadAllTextAsync(filename);
        }
    }
}
