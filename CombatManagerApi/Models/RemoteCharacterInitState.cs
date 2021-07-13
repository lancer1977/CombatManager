using System;
using System.Collections.Generic;

namespace CombatManagerApi
{
    public class CharacterInitState
    {
        public string Name { get; set; }
        public Guid ID { get; set; }
        public InitiativeCount InitiativeCount { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public bool IsMonster { get; set; }
        public bool IsActive { get; set; }
        public bool IsHidden { get; set; }

        public List<ActiveCondition> ActiveConditions { get; set; }
    }
}
