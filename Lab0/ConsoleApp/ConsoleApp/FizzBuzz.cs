using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class FizzBuzz
    {
        private int HighestNum;
        public FizzBuzz(int highestNum)
        {
            HighestNum = highestNum;
        }

        public void FizBuz()
        {
            for (int i = 1; i <= HighestNum; i++)
            {
                var str = "";
                if (i % 3 == 0)
                {
                    str += "fizz";
                }
                if (i % 5 == 0)
                {
                    str += "buzz";
                }
                if (str == "")
                {
                    str += i.ToString();
                }
                Console.WriteLine(str);
            }
        }
    }
}
