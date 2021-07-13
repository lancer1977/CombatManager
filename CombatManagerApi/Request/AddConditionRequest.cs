namespace CombatManagerApi.Request
{
    public class AddConditionRequest : CharacterRequest
    {
        public string Name { get; set; }
        public int? Turns { get; set; }
    }
}
