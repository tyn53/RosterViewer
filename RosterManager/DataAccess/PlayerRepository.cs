using Gmi.RosterManager.Controllers;
using Gmi.RosterManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gmi.RosterManager.DataAccess
{
    /// <summary>
    /// Data access repository for Player objects.
    /// </summary>
    public class PlayerRepository : IPlayerRepository
    {
        public IEnumerable<Player> GetPlayers()
        {
            var dbContext = new RosterManagerDataContext();

            var players = from player in dbContext.Players
                          orderby player.playerScreenName
                          select player;

            return players;
        }

        public Player GetPlayerById(int id)
        {
            var dbContext = new RosterManagerDataContext();

            var player = (from p in dbContext.Players
                          where p.playerId == id
                          select p).First();

            return player;
        }

        public void AddPlayer(Player player)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                dbContext.Players.InsertOnSubmit(player);
                dbContext.SubmitChanges();
            }
        }

        public void EditPlayer(PlayerModel player)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                var dbPlayer = (from p in dbContext.Players
                                where p.playerId == player.PlayerId
                                select p).First();

                if (player.AvatarImageFile != null)
                {
                    var unwantedImage = dbPlayer.Image;
                    if (unwantedImage != null)
                        dbContext.Images.DeleteOnSubmit(unwantedImage);
                    dbPlayer.Image = new Image()
                    {
                        imageFileName = player.AvatarImageFile.FileName,
                        imageContent = ImageController.ConvertToBytes(player.AvatarImageFile),
                        imageContentType = player.AvatarImageFile.ContentType,
                    };
                }

                dbPlayer.playerFirstName = player.FirstName;
                dbPlayer.playerLastName = player.LastName;
                dbPlayer.playerScreenName = player.ScreenName;
                dbPlayer.teamId = player.TeamId;
                
                dbContext.SubmitChanges();
                
            }
        }

        public void DeletePlayer(int playerId)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                var unwantedPlayer = (from player in dbContext.Players
                                where player.playerId == playerId
                                select player).First();

                var unwantedImage = unwantedPlayer.Image;
                if (unwantedImage != null)
                    dbContext.Images.DeleteOnSubmit(unwantedImage);

                foreach(var stat in unwantedPlayer.Stats)
                {
                    dbContext.Stats.DeleteOnSubmit(stat);
                }

                dbContext.Players.DeleteOnSubmit(unwantedPlayer);
                dbContext.SubmitChanges();
            }
        }
    }
    /// <summary>
    /// Player repository interface.
    /// </summary>
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetPlayers();
        Player GetPlayerById(int id);
        void AddPlayer(Player player);
        void EditPlayer(PlayerModel player);
        void DeletePlayer(int playerId);
    }
}