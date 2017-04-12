using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Gmi.RosterManager.Models
{
    public class TeamModel
    {
        private List<PlayerModel> players;
        private List<StatModel> stats;

        public int ID { get; set; }
        public string Name { get; set; }
        public int BannerImageId { get; set; }
        public HttpPostedFileBase BannerImageFile { get; set; }
        public List<PlayerModel> Players
        {
            get
            {
                players = players == null ? new List<PlayerModel>() : players;
                return players;
            }
            set
            {
                players = value;
            }
        }
        public List<StatModel> Stats
        {
            get
            {
                stats = stats == null ? new List<StatModel>() : stats;
                return stats;
            }
            set
            {
                stats = value;
            }
        }
    }
}