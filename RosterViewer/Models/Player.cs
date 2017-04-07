using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace RosterViewer.Models
{
    public class Player
    {
        private int playerId;
        private string playerScreenName;
        private string playerFirstName;
        private string playerLastname;
        private Image playerAvatar;
        private string currentTeam;
        private List<Stat> playerStats;

        public int PlayerId { get; set; }
        public string PlayerScreenName { get; set; }
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public Image PlayerAvatar { get; set; }
        public string CurrentTeam { get; set; }
        public List<Stat> PlayerStats { get; set; }

        
    }
}
