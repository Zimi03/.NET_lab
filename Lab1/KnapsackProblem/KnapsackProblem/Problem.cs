using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("TestProject1"), InternalsVisibleTo("GUI")]
namespace KnapsackProblem
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
                itemList[i] = new Item(i+1, random.Next(1, 10), random.Next(1, 10));
            }

        }

        public Problem(int n, Item[] items)
        {
            if (items.Length != n)
                throw new ArgumentException("Liczba przedmiotów nie zgadza się z podaną tablicą!");

            this.numOfItems = n;
            this.itemList = items;
        }

        public int getItemsLength()
        {
            return itemList.Length;
        }

        public Item[] GetItems()
        {
            return itemList;
        }

        public Result Solve(int capacity)
        {
            Array.Sort(itemList, (a, b) => b.ratio.CompareTo(a.ratio));
            Result result = new Result();
            Console.WriteLine();
            for (int i = 0; i < this.numOfItems; i++)
            {
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
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Lista przedmiotów:");
            foreach (Item item in itemList)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }

    }
}
