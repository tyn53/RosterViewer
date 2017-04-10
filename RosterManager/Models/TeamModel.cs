using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gmi.RosterManager.Models
{
    public class TeamModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] Banner { get; set; }
    }
}