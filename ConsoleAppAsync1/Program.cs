namespace ConsoleAppAsync1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int length = await DownloadHtmlAsync("https://www.baidu.com/", AppDomain.CurrentDomain.BaseDirectory + "test.txt");

            //异步方法的委托调用
            Task.Run(async delegate
            {
                while (true)
                {
                    await File.WriteAllTextAsync("", "");
                }
            });

            Console.WriteLine(length.ToString());

        }
        public static async Task<int> DownloadHtmlAsync(string url, string filename)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string str = await httpClient.GetStringAsync(url);
                File.WriteAllText(filename, str);
                return str.Length;
            }
        }

    }


}
