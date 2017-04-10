using Gmi.RosterManager.DataAccess;
using Gmi.RosterManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmi.RosterManager.Controllers
{
    public class TeamController : Controller
    {
        public ActionResult Team()
        {
            return View();
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(TeamModel model)
        {
            var teamRepo = new TeamRepository();

            HttpPostedFileBase file = Request.Files["ImageData"];
            model.Banner = ConvertToBytes(file);

            teamRepo.CreateTeam(model);

            return RedirectToAction("Index", "Teams");
        }

        public ActionResult GetImageById(int id)
        {
            var imageRepo = new ImageRepository();
            byte[] imageData = imageRepo.getImageById(id);

            if (imageData != null)
            {
                return File(imageData, "image/png");
            }
            else
            {
                return null;
            }
        }

        public static TeamModel ConvertToViewModel(Team dbTeam)
        {
            var team = new TeamModel()
            {
                ID = dbTeam.teamId,
                Name = dbTeam.teamName,
                Banner = dbTeam.Image?.imageData.ToArray(),
            };
            return team;
        }
        public static byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageData;
            var reader = new BinaryReader(image.InputStream);
            imageData = reader.ReadBytes((int)image.ContentLength);
            return imageData;
        }
    }
}