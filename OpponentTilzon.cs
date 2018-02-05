using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    [Serializable]
    class OpponentTilzon : AbstractCountry
    {

        public OpponentTilzon(string name = "Тильзон") : base(name)
        {
            // CountryName = name;
        }
        public override string ToString()
        {
            return "Это Тильзон";
        }
    }
}
