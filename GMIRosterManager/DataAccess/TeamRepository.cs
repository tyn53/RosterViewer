using GMIRosterManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMIRosterManager.DataAccess
{
    public class TeamRepository
    {
        public TeamRepository()
        {
        }

        public IEnumerable<Team> GetAllTeams()
        {
            var dbContext = new RosterManagerDataContext();
            IEnumerable<Team> teams;
                teams = from team in dbContext.Teams
                        join image in dbContext.Images on team.imageId equals image.imageId into teamImage
                        from subTeam in teamImage.DefaultIfEmpty()
                        orderby team.teamName
                        select team;
            return teams;
        
        }
        
        public void CreateTeam(TeamModel team)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                var dbTeam = new Team()
                {
                    teamName = team.Name,
                    Image = new Image()
                    {
                        imageData = team.Banner,
                        imageFileName = team.Name + "Banner.png",
                    },

                };

                dbContext.Teams.InsertOnSubmit(dbTeam);
                dbContext.SubmitChanges();
            }
        }

        public void UpdateTeam(Team team)
        {
            
        }

        public void DeleteTeam(Team team)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                dbContext.Teams.DeleteOnSubmit(team);
                dbContext.SubmitChanges();
            }
        }




    }
}