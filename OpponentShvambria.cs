using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    [Serializable]
    class OpponentShvambria : AbstractCountry
    {
        public OpponentShvambria(string name = "Швамбрия") : base(name)
        {
            // CountryName = name;
        }
        public override string ToString()
        {
            return "Это Швамбрия";
        }

    }
}
