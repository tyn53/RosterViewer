using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gmi.RosterManager.DataAccess
{
    public class PlayerRepository
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

        public void EditPlayer(Player player)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                var dbPlayer = (from p in dbContext.Players
                                where p.playerId == player.playerId
                                select p).First();

                dbPlayer.playerFirstName = player.playerFirstName;
                dbPlayer.playerLastName = player.playerLastName;
                dbPlayer.playerScreenName = player.playerScreenName;
                dbPlayer.Image = player.Image;
                dbPlayer.Team = player.Team;
                dbPlayer.Stats = player.Stats;

                dbContext.SubmitChanges();
                
            }
        }

        public void DeletePlayer(int playerId)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                var dbPlayer = (from p in dbContext.Players
                                where p.playerId == playerId
                                select p).First();

                dbContext.Players.DeleteOnSubmit(dbPlayer);
                dbContext.SubmitChanges();
            }
        }
    }
}