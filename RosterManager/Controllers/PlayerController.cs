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
        private readonly IPlayerRepository playerRepo;
        private readonly ITeamRepository teamRepo;

        public PlayerController(IPlayerRepository pRepo, ITeamRepository tRepo)
        {
            playerRepo = pRepo;
            teamRepo = tRepo;
        }

        public ActionResult Index()
        {
            return Get();
        }

        public ActionResult Get()
        {
            var players = new List<PlayerModel>();
          
            foreach (var player in playerRepo.GetPlayers())
            {
                players.Add(ConvertToViewModel(player));
            }


            return View(players);
        }

        public ActionResult Add()
        {
            var teams = teamRepo.GetTeams();

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

        public ActionResult Edit(int id)
        {
            var teams = teamRepo.GetTeams();

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

            var player = playerRepo.GetPlayerById(id);

            return View(ConvertToViewModel(player));
        }

        public ActionResult Details(int id)
        {
            var player = playerRepo.GetPlayerById(id);

            return View(ConvertToViewModel(player));
        }

        public ActionResult Delete(int id)
        {
            playerRepo.DeletePlayer(id);
            return RedirectToAction("Index", "Player");
        }

        [HttpPost]
        public ActionResult Update(PlayerModel player)
        {
            if (ImageController.ValidateImage(player.AvatarImageFile))
            {

                playerRepo.EditPlayer(player);

                return RedirectToAction("Details", "Team", new { ID = player.TeamId });
            }
            else
            {
                ViewBag.Error = "Invalid image. Image must be less than 4MB and a .jpg, .gif, .png or .svg";
                return RedirectToAction("Add", "Player");
            }

        }

        [HttpPost]
        public ActionResult Create(PlayerModel player)
        {
            if (ImageController.ValidateImage(player.AvatarImageFile))
            {
                playerRepo.AddPlayer(ConvertToDbModel(player));

                return RedirectToAction("Details", "Team", new { ID = player.TeamId });
            }
            else
            {
                ViewBag.Error = "Invalid image. Image must be less than 4MB and a .jpg, .gif, .png or .svg";
                return RedirectToAction("Add", "Player");
            }

        }

        public static PlayerModel ConvertToViewModel(Player dbPlayer)
        {
            var player = new PlayerModel()
            {
                PlayerId = dbPlayer.playerId,
                FirstName = dbPlayer.playerFirstName,
                LastName = dbPlayer.playerLastName,
                ScreenName = dbPlayer.playerScreenName,
                AvatarImageId = dbPlayer.imageId == null ? -1 : (int)dbPlayer.imageId,
                TeamName = dbPlayer.Team.teamName,
                TeamId = dbPlayer.teamId == null ? -1 : (int)dbPlayer.teamId,
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
                playerId = player.PlayerId,
                playerFirstName = player.FirstName,
                playerLastName = player.LastName,
                playerScreenName = player.ScreenName,
                Image = player.AvatarImageFile != null ? new Image()
                {
                    imageFileName = player.AvatarImageFile.FileName,
                    imageContent = ImageController.ConvertToBytes(player.AvatarImageFile),
                    imageContentType = player.AvatarImageFile.ContentType,

                } : null,
                teamId = player.TeamId,
            };

            foreach (var stat in player.Stats)
            {
                dbPlayer.Stats.Add(StatController.ConvertToDbModel(stat));
            }


            return dbPlayer;
        }
    }
}