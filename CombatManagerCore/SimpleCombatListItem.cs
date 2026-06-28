using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CombatManager
{
    [DataContract]
    public class SimpleCombatListItem
    {
        [DataMember]
        public Guid ID {get; set;}

        [DataMember]
        public List<Guid> Followers { get; set; }
    }
}