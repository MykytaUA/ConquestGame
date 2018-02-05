using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    [Serializable]
    class MyCountry : AbstractCountry
    {
        //new public string CountryName;
        public MyCountry(string name) : base(name)
        {


        }


        public override string ToString()
        {
            return "Ваша страна называется " + CountryName;
        }
    }
}
