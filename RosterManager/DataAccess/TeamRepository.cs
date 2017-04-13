using Gmi.RosterManager.Controllers;
using Gmi.RosterManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gmi.RosterManager.DataAccess
{
    /// <summary>
    /// Data access repository for team objects.
    /// </summary>
    public class TeamRepository : ITeamRepository
    {
        public TeamRepository()
        {
        }

        public IEnumerable<Team> GetTeams()
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

        public void AddTeam(Team team)
        {
            using (var dbContext = new RosterManagerDataContext())
            {                
                dbContext.Teams.InsertOnSubmit(team);

                dbContext.SubmitChanges();
            }
        }

        public void EditTeam(TeamModel team)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                var dbTeam = dbContext.Teams.Single(t => t.teamId == team.TeamId);
                
                if (team.BannerImageFile != null)
                {
                    var unwantedImage = dbTeam.Image;
                    dbContext.Images.DeleteOnSubmit(unwantedImage);
                    dbTeam.Image = new Image()
                    {
                        imageFileName = team.BannerImageFile.FileName,
                        imageContent = ImageController.ConvertToBytes(team.BannerImageFile),
                        imageContentType = team.BannerImageFile.ContentType,
                    };
                }

                dbTeam.teamName = team.Name;
                
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

                foreach(var player in unwantedTeam.Players)
                {
                    dbContext.Players.DeleteOnSubmit(player);
                }

                foreach(var stat in unwantedTeam.Stats)
                {
                    dbContext.Stats.DeleteOnSubmit(stat);
                }

                dbContext.Teams.DeleteOnSubmit(unwantedTeam);

                
                dbContext.SubmitChanges();
            }
        }
    }

    /// <summary>
    /// Team repository interface.
    /// </summary>
    public interface ITeamRepository
    {
        IEnumerable<Team> GetTeams();
        Team GetTeamByID(int id);
        void AddTeam(Team team);
        void EditTeam(TeamModel team);
        void DeleteTeam(int id);

    }
}