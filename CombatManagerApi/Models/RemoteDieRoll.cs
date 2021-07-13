using System.Collections.Generic;

namespace CombatManagerApi
{
    public class DieRoll
    {
        public int Mod {get; set;}
        public int Fraction {get; set;}

        public List<Die> Dice {get; set;}
    }
}
