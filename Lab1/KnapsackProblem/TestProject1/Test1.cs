using KnapsackProblem;

namespace TestProject1
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
            
            Assert.IsTrue(number >= 1);
        }

        [TestMethod]
        public void TestEmpty()
        {
            int n = 3;
            Item[] items = new Item[]
            {
                new Item (1, 1, 1),
                new Item (2, 2, 2),
                new Item (3, 3, 3),
            };
            int capacity = 0;
            Problem problem = new Problem(n, items);
            Result knappackResult = problem.Solve(capacity);
            List<int> resultIds = knappackResult.getItemIds();
            int number = resultIds.Count;

            Assert.IsTrue(number == 0);
        }

        [TestMethod]
        public void TestInstance()
        {
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
            Result result = problem.Solve(capacity);
            int expectedWeight = 8;
            int expectedValue = 15;
            List<int> expectedIds = new List<int>() { 5, 2, 3 };
            

            CollectionAssert.AreEqual(result.getItemIds(), expectedIds);
            Assert.AreEqual(result.getTotalValue(), expectedValue);
            Assert.AreEqual(result.getTotalWeight(), expectedWeight);

        }
    }
}
