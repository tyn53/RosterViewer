using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gmi.RosterManager.Models
{
    public class PlayerModel
    {
        private List<StatModel> stats;

        public int PlayerId { get; set; }
        public string ScreenName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int TeamBannerImageId { get; set; }
        public int AvatarImageId { get; set; }
        public HttpPostedFileBase AvatarImageFile { get; set; }
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