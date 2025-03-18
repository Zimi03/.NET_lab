using KnapsackProblem;

namespace TestProject
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestResult()
        {
            int n = 5;
            Item[] items = new Item[]
            {
                new Item (1, 10, 5),
                new Item (2, 2, 3),
                new Item (3, 3, 2),
                new Item (4, 1, 3),
                new Item (5, 2, 2)
            };
            int capacity = 10;
            Problem problem = new Problem(n, items);
            Result knappackResult = problem.Solve(capacity);
            List<int> resultIds = knappackResult.getItemIds();
            int number = resultIds.Count;
            Console.WriteLine($"liczba wybranych przedmiotow:{number}");
            Assert.IsTrue(number < 1);   
        }
    }
}
