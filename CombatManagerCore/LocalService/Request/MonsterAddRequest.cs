﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatManager.LocalService.Request
{
    public class MonsterAddRequest
    {
        public string Name { get; set; }
        public bool IsMonster { get; set; }
        public List<MonsterRequest> Monsters { get; set; }

    }
}
