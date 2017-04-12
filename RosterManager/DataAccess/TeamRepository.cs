using Gmi.RosterManager.Controllers;
using Gmi.RosterManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gmi.RosterManager.DataAccess
{
    public class TeamRepository
    {
        public TeamRepository()
        {
        }

        public IEnumerable<Team> GetAllTeams()
        {
            var dbContext = new RosterManagerDataContext();

            var teams = from team in dbContext.Teams
                        join image in dbContext.Images on team.imageId equals image.imageId into teamImage
                        from subTeam in teamImage.DefaultIfEmpty()
                        orderby team.teamName
                        select team;
            return teams;

        }

        public Team GetTeamByID(int id)
        {
            var dbContext = new RosterManagerDataContext();

            var result = (from team in dbContext.Teams
                          where team.teamId == id
                          select team).First();

            return result;
        }

        public void CreateTeam(Team team)
        {
            using (var dbContext = new RosterManagerDataContext())
            {                
                dbContext.Teams.InsertOnSubmit(team);

                dbContext.SubmitChanges();
            }
        }

        public void UpdateTeam(Team team)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                var dbTeam = (from t in dbContext.Teams
                              where t.teamId == team.teamId
                              select t).First();

                if (team.Image != null)
                {
                    var unwantedImage = dbTeam.Image;
                    dbContext.Images.DeleteOnSubmit(unwantedImage);
                    dbTeam.Image = team.Image;
                }
                
                dbTeam.teamName = team.teamName;
                

                dbContext.SubmitChanges();
            }
        }

        public void DeleteTeam(int id)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                var unwantedTeam = (from team in dbContext.Teams
                                    where team.teamId == id
                                    select team).First();

                var unwantedImage = unwantedTeam.Image;
                if (unwantedImage != null)
                    dbContext.Images.DeleteOnSubmit(unwantedImage);

                dbContext.Teams.DeleteOnSubmit(unwantedTeam);

                
                dbContext.SubmitChanges();
            }
        }




    }
}