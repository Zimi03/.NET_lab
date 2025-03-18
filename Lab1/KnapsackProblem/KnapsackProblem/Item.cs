using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    class Item
    {
        public int id;
        public int value;
        public int weight;
        public float ratio;
        public Item(int id, int value, int weight)
        {
            this.id = id;
            this.value = value;
            this.weight = weight;
            this.ratio = (float)value / weight;
        }
        public override string ToString()
        {
            return $"Id: {id}, V: {value}, W: {weight}";
        }
    }
}
