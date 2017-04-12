using Gmi.RosterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gmi.RosterManager.DataAccess
{
    public class StatRepository : IStatRepository
    {

        public IEnumerable<Stat> GetStatsByEntity(int entityId)
        {
            var dbContext = new RosterManagerDataContext();

            var stats = from stat in dbContext.Stats
                        where stat.entityId == entityId
                        orderby stat.statName
                        select stat;

            return stats;
        }

        public Stat GetStatById(int id)
        {
            var dbContext = new RosterManagerDataContext();

            var stat = (from s in dbContext.Stats
                          where s.statId == id
                          select s).First();

            return stat;
        }

        public void AddStat(Stat stat)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                dbContext.Stats.InsertOnSubmit(stat);
                dbContext.SubmitChanges();
            }
        }

        public void EditStat(StatModel stat)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                var dbStat = (from s in dbContext.Stats
                                where s.statId == stat.StatId
                                select s).First();

                dbStat.statName = stat.Name;
                dbStat.statValue = stat.Value;
                
                dbContext.SubmitChanges();

            }
        }

        public void DeleteStat(int statId)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                var dbStat = (from s in dbContext.Stats
                                where s.statId == statId
                                select s).First();

                dbContext.Stats.DeleteOnSubmit(dbStat);
                dbContext.SubmitChanges();
            }
        }
    }

    public interface IStatRepository
    {
        IEnumerable<Stat> GetStatsByEntity(int entityId);
        Stat GetStatById(int id);
        void AddStat(Stat stat);
        void EditStat(StatModel stat);
        void DeleteStat(int statId);
    }
}