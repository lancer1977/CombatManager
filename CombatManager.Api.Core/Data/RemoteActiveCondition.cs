namespace CombatManager.Api.Core.Data
{
    public class RemoteActiveCondition
    {
        public RemoteConditon Conditon { get; set; }
        public int? Turns { get; set; }
        public RemoteInitiativeCount InitiativeCount { get; set; }
        public string Details { get; set; }
    }

    public class RemoteServiceMessage
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public object Data { get; set; }
    }
}
