using System.Collections.Generic;

namespace CombatManagerApi
{
    public class RollResult
    {
        public int Total { get; set; }

        public List<DieResult> Rolls { get; set; }

        public int Mod { get; set; }

    }
}
