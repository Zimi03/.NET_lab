namespace APIConnect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            TestAPI t = new TestAPI();
            t.GetData().Wait();
        }
    }
}
