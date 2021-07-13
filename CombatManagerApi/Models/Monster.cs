using System;
using System.Collections.Generic;

namespace CombatManagerApi
{
    public class Monster
    {
        public string Name { get; set; }
        public string CR { get; set; }
        public string XP { get; set; }
        public string Race { get; set; }
        public string ClassName { get; set; }
        public string Alignment { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public int Init { get; set; }
        public int? DualInit { get; set; }
        public string Senses { get; set; }
        public int AC { get; set; }
        public string ACMods { get; set; }
        public int HP { get; set; }
        public string HDText { get; set; }
        public DieRoll HD { get; set; }
        public int? Fort { get; set; }
        public int? Ref { get; set; }
        public int? Will { get; set; }
        public string SaveMods { get; set; }
        public string Resist { get; set; }
        public string DR { get; set; }
        public string SR { get; set; }
        public string Speed { get; set; }
        public string Melee { get; set; }
        public string Ranged { get; set; }
        public int Space { get; set; }
        public int Reach { get; set; }
        public string SpecialAttacks { get; set; }
        public string SpellLikeAbilities { get; set; }
        public int? Strength { get; set; }
        public int? Dexterity { get; set; }
        public int? Constitution { get; set; }
        public int? Intelligence { get; set; }
        public int? Wisdom { get; set; }
        public int? Charisma { get; set; }
        public int BaseAtk { get; set; }
        public int CMB { get; set; }
        public int CMD { get; set; }
        public string Feats { get; set; }
        public string Skills { get; set; }
        public string RacialMods { get; set; }
        public string Languages { get; set; }
        public string SQ { get; set; }
        public string Environment { get; set; }
        public string Organization { get; set; }
        public string Treasure { get; set; }
        public string DescriptionVisual { get; set; }
        public string Group { get; set; }
        public string Source { get; set; }
        public string IsTemplate { get; set; }
        public string SpecialAbilities { get; set; }
        public string Description { get; set; }
        public string FullText { get; set; }
        public string Gender { get; set; }
        public string Bloodline { get; set; }
        public string ProhibitedSchools { get; set; }
        public string BeforeCombat { get; set; }
        public string DuringCombat { get; set; }
        public string Morale { get; set; }
        public string Gear { get; set; }
        public string OtherGear { get; set; }
        public string Vulnerability { get; set; }
        public string Note { get; set; }
        public string CharacterFlag { get; set; }
        public string CompanionFlag { get; set; }
        public int? FlySpeed { get; set; }
        public int? ClimbSpeed { get; set; }
        public int? BurrowSpeed { get; set; }
        public int? SwimSpeed { get; set; }
        public int LandSpeed { get; set; }
        public string TemplatesApplied { get; set; }
        public string OffenseNote { get; set; }
        public string BaseStatistics { get; set; }
        public string SpellsPrepared { get; set; }
        public string SpellDomains { get; set; }
        public string Aura { get; set; }
        public string DefensiveAbilities { get; set; }
        public string Immune { get; set; }
        public string HPMods { get; set; }
        public string SpellsKnown { get; set; }
        public string Weaknesses { get; set; }
        public string SpeedMod { get; set; }
        public string MonsterSource { get; set; }
        public string ExtractsPrepared { get; set; }
        public string AgeCategory { get; set; }
        public bool DontUseRacialHD { get; set; }
        public string VariantParent { get; set; }
        public bool NPC { get; set; }
        public int? MR { get; set; }
        public string Mythic { get; set; }

        public int TouchAC { get; set; }
        public int FlatFootedAC { get; set; }
        public int NaturalArmor { get; set; }
        public int Shield { get; set; }
        public int Armor { get; set; }
        public int Dodge { get; set; }
        public int Deflection { get; set; }

        public List<ActiveCondition> ActiveConditions { get; set; }


    }
}
