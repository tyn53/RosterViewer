using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;

namespace RosterViewer.Models
{
    [Table(Name="Team")]
    public class Team
    {
        private int teamId;
        private string teamName;
        private List<Player> teamRoster;
        private Image teamBanner;

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public List<Player> TeamRoster { get; set; }
        public Image TeamBanner { get; set; }
    }
}
