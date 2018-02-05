using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    [Serializable]
    public abstract class AbstractCountry
    {
        public AbstractCountry(string name)
        {
            CountryName = name;
        }
        public bool Alive = true;
        public readonly string CountryName;
        public int Food { get; set; } = 1000;
        public int Gold { get; set; } = 1000;
        public int AllPeople = 1000;
        public int FreePeople { get; set; } = 1000;
        public int MaxArmy { get; set; }
        public int Army { get; set; }
        public int ArmyOnAttack { get; set; } = 0;
        public int ArmyOnDef { get; set; }
        public int BusinessPeople { get; set; }
        public int AttackOrNot { get; set; }
        public int EnemyCountry { get; set; }
        public int MaxBusiness { get; set; }
    }
}
