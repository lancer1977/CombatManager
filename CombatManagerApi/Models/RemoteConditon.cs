using System;

namespace CombatManagerApi
{
    public class Conditon
    {

        public string Name {get; set;}
        public string Text {get; set;}
        public string Image {get; set;}
        public Spell Spell {get; set;}
        public Affliction Affliction {get; set;}
        public Bonus Bonus {get; set;}
        public bool Custom {get; set;}
    }
}
