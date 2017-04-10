using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMIRosterManager.DataAccess;
using GMIRosterManager.Models;
using System.IO;

namespace GMIRosterManager.Controllers
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