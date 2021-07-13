using System;
using System.Collections.Generic;

namespace CombatManagerApi
{
    public class Character
    {
        public Guid ID {get; set;}
        public string Name {get; set;}
        public int HP {get; set;}
        public int MaxHP {get; set;}
        public int NonlethalDamage {get; set;}
        public int TemporaryHP {get; set;}
        public string Notes {get; set;}
        public bool IsMonster {get; set;}
        public bool IsReadying {get; set;}
        public bool IsDelaying {get; set;}
        public uint? Color {get; set;}
        public bool IsActive {get; set;}
        public bool IsIdle {get; set;}
        public InitiativeCount InitiativeCount {get; set;}
        public int InitiativeRolled {get; set;}
        public Guid? InitiativeLeader {get; set;}
        public List<Guid> InitiativeFollowers {get; set;}
        public Monster Monster {get; set;}


    }
}
