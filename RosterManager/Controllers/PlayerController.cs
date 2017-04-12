using Gmi.RosterManager.DataAccess;
using Gmi.RosterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmi.RosterManager.Controllers
{
    public class PlayerController : Controller
    {
        // GET: Player
        public ActionResult Index()
        {
            return Get();
        }

        public ActionResult Get()
        {
            var playerRepo = new PlayerRepository();
            var players = new List<PlayerModel>();
          
            foreach (var player in playerRepo.GetPlayers())
            {
                players.Add(ConvertToViewModel(player));
            }


            return View(players);
        }

        public ActionResult Add()
        {
            var teamRepo = new TeamRepository();
            var teams = teamRepo.GetAllTeams();

            var teamList = new List<SelectListItem>();

            foreach (var team in teams)
            {
                teamList.Add(new SelectListItem()
                {
                    Text = team.teamName,
                    Value = team.teamId.ToString(),
                });
            }

            ViewData["Teams"] = teamList;

            
            return View();
        }

        public ActionResult Details(int id)
        {
            var playerRepo = new PlayerRepository();
            var player = playerRepo.GetPlayerById(id);

            return View(ConvertToViewModel(player));
        }

        public ActionResult Delete(int id)
        {
            var playerRepo = new PlayerRepository();
            playerRepo.DeletePlayer(id);
            return RedirectToAction("Index", "Player");
        }

        [HttpPost]
        public ActionResult Create(PlayerModel player)
        {
            var playerRepo = new PlayerRepository();

            playerRepo.AddPlayer(ConvertToDbModel(player));

            return RedirectToAction("Details", "Team", new { ID = player.TeamID });

        }

        public static PlayerModel ConvertToViewModel(Player dbPlayer)
        {
            var player = new PlayerModel()
            {
                PlayerID = dbPlayer.playerId,
                FirstName = dbPlayer.playerFirstName,
                LastName = dbPlayer.playerLastName,
                ScreenName = dbPlayer.playerScreenName,
                AvatarImageID = dbPlayer.imageId == null ? -1 : (int)dbPlayer.imageId,
                TeamName = dbPlayer.Team.teamName,
                TeamID = dbPlayer.teamId == null ? -1 : (int)dbPlayer.teamId,
            };

            foreach(var stat in dbPlayer.Stats)
            {
                player.Stats.Add(StatController.ConvertToViewModel(stat));
            }
                        
            return player;
        }

        public static Player ConvertToDbModel(PlayerModel player)
        {
            var dbPlayer = new Player()
            {
                playerId = player.PlayerID,
                playerFirstName = player.FirstName,
                playerLastName = player.LastName,
                playerScreenName = player.ScreenName,
                Image = player.AvatarImageFile != null ? new Image()
                {
                    imageFileName = player.AvatarImageFile.FileName,
                    imageContent = ImageController.ConvertToBytes(player.AvatarImageFile),
                    imageContentType = player.AvatarImageFile.ContentType,

                } : null,
                teamId = player.TeamID,
            };

            foreach (var stat in player.Stats)
            {
                dbPlayer.Stats.Add(StatController.ConvertToDbModel(stat));
            }


            return dbPlayer;
        }
    }
}