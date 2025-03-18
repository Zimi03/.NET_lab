namespace KnapsackProblem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Problem knapsack = new Problem(10, 1);
            Result result = knapsack.Solve(20);
            Console.WriteLine(result.ToString());
            int n = 5;
            Item[] items = new Item[]
            {
                new Item (1, 2, 3),
                new Item (2, 3, 2),
                new Item (3, 5, 4),
                new Item (4, 10, 9),
                new Item (5, 7, 2)
            };
            int capacity = 10;
            Problem problem = new Problem(n, items);
            Result result_1 = problem.Solve(capacity);
            Console.WriteLine(result_1.ToString());
        }
    }
}
