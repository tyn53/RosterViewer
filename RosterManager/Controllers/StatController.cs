using Gmi.RosterManager.DataAccess;
using Gmi.RosterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmi.RosterManager.Controllers
{
    public class StatController : Controller
    {
        public ActionResult Add(int entityId, Models.EntityType type)
        {
            var stat = new StatModel();
            stat.entityID = entityId;
            stat.EntityType = type;
            return View(stat);
        }

        public ActionResult Edit(int statId)
        {
            var statRepo = new StatRepository();
            var stat = statRepo.GetStatById(statId);

            return View(stat);
        }

        public ActionResult Delete(int statId)
        {
            
            var statRepo = new StatRepository();
            var tempStat = statRepo.GetStatById(statId);
            statRepo.DeleteStat(statId);
            return RedirectToAction("Details", ConvertToViewModel(tempStat).EntityType.ToString(), new { ID = ConvertToViewModel(tempStat).entityID });
        }

        [HttpPost]
        public ActionResult Create(StatModel stat)
        {
            var statRepo = new StatRepository();
            
            statRepo.AddStat(ConvertToDbModel(stat));

            return RedirectToAction("Details", stat.EntityType.ToString(), new { ID = stat.entityID });
        }

        [HttpPost]
        public ActionResult Update(StatModel stat)
        {
            var statRepo = new StatRepository();

            statRepo.EditStat(ConvertToDbModel(stat));

            return RedirectToAction("Details", stat.EntityType.ToString(), new { ID = stat.entityID });
        }

        public static StatModel ConvertToViewModel(Stat dbStat)
        {
            var stat = new StatModel()
            {
                StatID = dbStat.statId,
                Name = dbStat.statName,
                Value = dbStat.statValue,
                entityID = dbStat.entityId,
                EntityType = (Models.EntityType)dbStat.entityTypeId,
            };

            return stat;
        }

        public static Stat ConvertToDbModel(StatModel stat)
        {
            var dbStat = new Stat()
            {
                statName = stat.Name,
                statValue = stat.Value,
                entityId = stat.entityID,
                entityTypeId = (int)stat.EntityType,
            };

            return dbStat;
        }
    }
}