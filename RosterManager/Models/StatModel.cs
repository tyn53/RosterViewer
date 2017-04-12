using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gmi.RosterManager.Models
{
    public class StatModel
    {
        public int StatID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int entityID { get; set; }
        public EntityType EntityType { get; set; }
    }

    public enum EntityType
    {
        Team = 1,
        Player = 2,
    }
}