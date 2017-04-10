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
    public class TeamsController : Controller
    {
        // GET: Team
        public ActionResult Index()
        {
            return Get();
        }

        public ActionResult Get()
        {
            var teamRepo = new TeamRepository();
            var teams = new TeamsModel();
            teams.Teams = new List<TeamModel>();
            foreach (var team in teamRepo.GetAllTeams())
            {
                teams.Teams.Add(TeamController.ConvertToViewModel(team));
            }
            

            return View(teams);
        }
    }
}