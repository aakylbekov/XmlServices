using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLib.Module
{
    public class Provider
    {
        public List<int> Prefix { get; set; } = new List<int>(); 
        public string LogUrl { get; set; }
        public string Name { get; set; }
        public double Percent { get; set; }
        public Provider()
        {

        }
    }
}
