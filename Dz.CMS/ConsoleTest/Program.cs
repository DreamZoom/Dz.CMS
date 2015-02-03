using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.ReadKey();

        }
    }

    public class TestModel : Dz.CMS.Model.ModelBase
    {
        public String Name { get; set; }
        public String Age { get; set; }
        public int Count { get; set; }
        public int Count1 { get; set; }
        public int Count2 { get; set; }
    }

    public class TestModel2 : Dz.CMS.Model.ModelBase
    {
        public String Name { get; set; }
        public String Age { get; set; }
        public int Count { get; set; }
        public int Count1 { get; set; }
        public int Count2 { get; set; }
    }
}
