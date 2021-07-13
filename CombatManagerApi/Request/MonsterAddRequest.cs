using System.Collections.Generic;

namespace CombatManagerApi.Request
{
    public class MonsterAddRequest
    {
        public string Name { get; set; }
        public bool IsMonster { get; set; }
        public List<MonsterRequest> Monsters { get; set; }

    }
}
