using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConnect
{
    public class Currencies
    {
        public long timestamp { get; set; }
        public string? Base { get; set; }
        public Dictionary<string, float>? rates { get; set; }  

        public override string ToString()
        {
            return $"Timestamp: {timestamp}, Base: {Base}, First Rate: {{rates?.FirstOrDefault().Key}} - {{rates?.FirstOrDefault().Value";
        }
        

    }
}
