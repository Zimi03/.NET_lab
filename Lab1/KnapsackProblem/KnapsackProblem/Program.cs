namespace KnapsackProblem
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            Problem knapsack = new Problem(10, 1);
            Console.WriteLine(knapsack.ToString());
            Result result = knapsack.Solve(20);
            Console.WriteLine(result.ToString());
        }
    }
}
