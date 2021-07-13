using System;
using System.Collections.Generic;

namespace CombatManagerApi
{
    public class CombatState
    {
        public int? Round { get; set; }
        public string CR { get; set; }
        public long? XP { get; set; }
        public int RulesSystem { get; set; }

        public List<CharacterInitState> CombatList { get; set; }

        public Guid CurrentCharacterID { get; set; }
        public InitiativeCount CurrentInitiativeCount { get; set; } 

    }
}
