using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RosterViewer.Models
{
    public class Stat
    {
        private int statId;
        private string statName;
        private string statValue;

        public int StatId { get; set; }
        public string StatName { get; set; }
        public string StatValue { get; set; }
    }
}
