﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatManager.LocalService.Request
{
    public class MonsterListRequest
    {
        public string Name { get; set; }
        public bool? IsCustom { get; set; }
        public bool? IsNPC { get; set; }
        public string MinCR { get; set; }
        public string MaxCR { get; set; }

    }
}