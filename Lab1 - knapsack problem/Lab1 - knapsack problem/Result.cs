using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1___knapsack_problem
{
    class Result
    {
        private List<int> itemIds;
        private int totalValue;
        private int totalWeight;

        public Result()
        {
            itemIds = new List<int>();
            totalValue = 0;
            totalWeight = 0;
        }

        public void AddItem(Item item)
        {
            itemIds.Add(item.id);
            totalValue += item.value;
            totalWeight += item.weight;
        }
        
        public int getTotalWeight()
        {
            return totalWeight;
        }
        public override string ToString()
        {
            return $"Items_ids: [{string.Join(", ", itemIds)}], Total Value: {totalValue}, Total Weight: {totalWeight}";
        }


    }
}
