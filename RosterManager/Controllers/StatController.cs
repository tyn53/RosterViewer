using Gmi.RosterManager.DataAccess;
using Gmi.RosterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmi.RosterManager.Controllers
{
    /// <summary>
    /// MVC Controller for statistic actions.
    /// </summary>
    public class StatController : Controller
    {
        private readonly IStatRepository statRepo;
        public StatController(IStatRepository sRepo)
        {
            this.statRepo = sRepo;
        }

        public ActionResult Add(int entityId, Models.EntityType type)
        {
            var stat = new StatModel();
            stat.EntityId = entityId;
            stat.EntityType = type;
            return View(stat);
        }

        public ActionResult Edit(int statId)
        {
            var stat = statRepo.GetStatById(statId);

            return View(ConvertToViewModel(stat));
        }

        public ActionResult Delete(int statId)
        {
            var tempStat = statRepo.GetStatById(statId);
            statRepo.DeleteStat(statId);
            return RedirectToAction("Details", ConvertToViewModel(tempStat).EntityType.ToString(), new { ID = ConvertToViewModel(tempStat).EntityId });
        }

        [HttpPost]
        public ActionResult Create(StatModel stat)
        {
            if (stat.Value == null)
                stat.Value = string.Empty;
            
            statRepo.AddStat(ConvertToDbModel(stat));
            
            return RedirectToAction("Details", stat.EntityType.ToString(), new { ID = stat.EntityId });
        }

        [HttpPost]
        public ActionResult Update(StatModel stat)
        {
            statRepo.EditStat(stat);

            return RedirectToAction("Details", stat.EntityType.ToString(), new { ID = stat.EntityId });
        }

        public static StatModel ConvertToViewModel(Stat dbStat)
        {
            var stat = new StatModel()
            {
                StatId = dbStat.statId,
                Name = dbStat.statName,
                Value = dbStat.statValue,
                EntityId = dbStat.entityId,
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
                entityId = stat.EntityId,
                entityTypeId = (int)stat.EntityType,
            };

            return dbStat;
        }
    }
}