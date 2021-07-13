namespace CombatManagerApi
{
    public class ActiveCondition
    {
        public Conditon Conditon { get; set; }
        public int? Turns { get; set; }
        public InitiativeCount InitiativeCount { get; set; }
        public string Details { get; set; }
    }
}
