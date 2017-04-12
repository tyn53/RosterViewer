using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gmi.RosterManager.Models
{
    public class StatModel
    {
        public int StatId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int EntityId { get; set; }
        public EntityType EntityType { get; set; }
    }

    public enum EntityType
    {
        Team = 1,
        Player = 2,
    }
}