using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("TestProject1 - knapsack")]
namespace Lab1___knapsack_problem
{
    class Problem
    {
        private int numOfItems;
        private Item[] itemList;

        public Problem(int n, int seed)
        {
            this.numOfItems = n;
            itemList = new Item[n];
            Random random = new Random(seed);
            for (int i = 0; i < n; i++)
            {
                itemList[i] = new Item(i, random.Next(1,10), random.Next(1,10));
               Console.WriteLine(itemList[i].ToString());
            }
           
        }

        public Result Solve(int capacity)
        {
            Array.Sort(itemList, (a, b) => b.ratio.CompareTo(a.ratio));
            Result result = new Result();
            Console.WriteLine();
            for (int i = 0; i < this.numOfItems; i++)
            {
                Console.WriteLine(itemList[i].ToString());
                int currentTotalWeight = result.getTotalWeight();
                if (currentTotalWeight + itemList[i].weight <= capacity)
                {
                    result.AddItem(itemList[i]);
                }
                else
                {
                    break;
                }
            }
            return result;
        }
    }
}
