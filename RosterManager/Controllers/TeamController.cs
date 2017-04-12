using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gmi.RosterManager.DataAccess;
using Gmi.RosterManager.Models;
using System.IO;

namespace Gmi.RosterManager.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository teamRepo;

        public TeamController(ITeamRepository repo)
        {
            teamRepo = repo;
        }

        // GET: Team
        public ActionResult Index()
        {
            return Get();
        }

        public ActionResult Get()
        {
            var teams = new List<TeamModel>();
            foreach (var team in teamRepo.GetTeams())
            {
                teams.Add(ConvertToViewModel(team));
            }


            return View(teams);
        }

        public ActionResult Details(int id)
        {
            var dbResult = teamRepo.GetTeamByID(id);

            return View(ConvertToViewModel(dbResult));
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var dbResult = teamRepo.GetTeamByID(id);
            return View(ConvertToViewModel(dbResult));
        }

        public ActionResult Delete(int id)
        {
            teamRepo.DeleteTeam(id);
            return RedirectToAction("Index", "Team");
        }

        [HttpPost]
        public ActionResult Create(TeamModel model)
        {

            teamRepo.AddTeam(ConvertToDbModel(model));

            return RedirectToAction("Index", "Team");
        }

        [HttpPost]
        public ActionResult Update(TeamModel model)
        {
            teamRepo.EditTeam(model);

            return RedirectToAction("Details", "Team", new { id = model.TeamId });
        }

        public static TeamModel ConvertToViewModel(Team dbTeam)
        {
            var team = new TeamModel()
            {
                TeamId = dbTeam.teamId,
                Name = dbTeam.teamName,
                BannerImageId = (dbTeam.imageId == null ? -1 : (int)dbTeam.imageId),
            };

            foreach(var player in dbTeam.Players)
            {
                team.Players.Add(PlayerController.ConvertToViewModel(player));
            }

            foreach(var stat in dbTeam.Stats)
            {
                team.Stats.Add(StatController.ConvertToViewModel(stat));
            }

            return team;
        }

        public static Team ConvertToDbModel(TeamModel team)
        {
            var dbTeam = new Team()
            {
                teamId = team.TeamId,
                teamName = team.Name,
                Image = team.BannerImageFile == null ? null : new Image()
                {
                    imageFileName = team.BannerImageFile.FileName,
                    imageContent = ImageController.ConvertToBytes(team.BannerImageFile),
                    imageContentType = team.BannerImageFile.ContentType
                },
            };

            foreach(var player in team.Players)
            {
                dbTeam.Players.Add(PlayerController.ConvertToDbModel(player));
            }
            
            foreach(var stat in team.Stats)
            {
                dbTeam.Stats.Add(StatController.ConvertToDbModel(stat));
            }

            return dbTeam;
        }
    }
}