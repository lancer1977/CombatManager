using System;

namespace CombatManagerApi
{
    public class Affliction
    {

        public string Name{get; set; }
        public string Type{get; set; }
        public string Cause{get; set; }
        public string SaveType{get; set; }
        public int Save{get; set; }
        public DieRoll Onset{get; set; }
        public string OnsetUnit{get; set; }
        public bool Immediate{get; set; }
        public int Frequency{get; set; }
        public string FrequencyUnit{get; set; }
        public int Limit{get; set; }
        public string LimitUnit{get; set; }
        public string SpecialEffectName{get; set; }
        public DieRoll SpecialEffectTime {get; set; }
        public string SpecialEffectUnit{get; set; }
        public string OtherEffect{get; set; }
        public bool Once{get; set; }
        public DieRoll DamageDie {get; set; }
        public string DamageType{get; set; }
        public bool IsDamageDrain{get; set; }
        public DieRoll SecondaryDamageDie {get; set; }
        public string SecondaryDamageType{get; set; }
        public bool IsSecondaryDamageDrain{get; set; }
        public string DamageExtra{get; set; }
        public string Cure{get; set; }
        public string Details{get; set; }
        public string Cost{get; set; }

    
        
    }
}
