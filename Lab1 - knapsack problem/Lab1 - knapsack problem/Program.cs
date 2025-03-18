namespace Lab1___knapsack_problem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Problem knapsack = new Problem(10, 1);
            Result result = knapsack.Solve(20);
            Console.WriteLine(result.ToString());
        }
    }
}
