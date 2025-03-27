using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConnect
{
    public class currRates
    {
        public string currShort { get; set; }
        public float value { get; set; }
        public override string ToString()
        {
            return $"Currency ID: {currShort}, value: {value}";
        }
    }
}
