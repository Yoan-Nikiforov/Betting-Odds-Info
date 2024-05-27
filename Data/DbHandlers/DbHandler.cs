using Microsoft.EntityFrameworkCore;
using UltraPlayTask.Data.Models;

namespace UltraPlayTask.Data.DbHandlers
{
    public class DbHandler : BaseDbHandler
    {
        public DbHandler(ApplicationDbContext context) : base(context)
        {
        }

        public List<Sport> GetSports()
        {
            return _context.Sports.Include(s => s.Events)
                                  .ThenInclude(e => e.Matches)
                                  .ThenInclude(m => m.Bets)
                                  .ThenInclude(b => b.Odds)
                                  .ToList();
        }

        public void SyncSport(Sport newSport)
        {
            var existingSport = _context.Sports.Include(s => s.Events)
            .ThenInclude(e => e.Matches)
            .ThenInclude(m => m.Bets)
            .ThenInclude(b => b.Odds)
            .FirstOrDefault(s => s.ID == newSport.ID);

            if (existingSport == null)
            {
                _context.Sports.Add(newSport);
                return;
            }

            foreach (var newEvent in newSport.Events)
            {
                var existingEvent = existingSport.Events.FirstOrDefault(e => e.ID == newEvent.ID);
                if (existingEvent == null)
                {
                    newEvent.SportID = existingSport.ID;
                    existingSport.Events.Add(newEvent);
                    continue;
                }

                newEvent.SportID = existingEvent.SportID;
                DetachAndUpdate(existingEvent, newEvent);

                foreach (var newMatch in newEvent.Matches)
                {
                    var existingMatch = existingEvent.Matches.FirstOrDefault(m => m.ID == newMatch.ID);
                    if (existingMatch == null)
                    {
                        newMatch.EventID = existingEvent.ID;
                        existingEvent.Matches.Add(newMatch);
                        continue;
                    }

                    newMatch.EventID = existingMatch.EventID;
                    DetachAndUpdate(existingMatch, newMatch);

                    foreach (var newBet in newMatch.Bets)
                    {
                        var existingBet = existingMatch.Bets.FirstOrDefault(b => b.ID == newBet.ID);
                        if (existingBet == null)
                        {
                            newBet.MatchID = existingMatch.ID;
                            existingMatch.Bets.Add(newBet);
                            continue;
                        }

                        newBet.MatchID = existingBet.MatchID;
                        DetachAndUpdate(existingBet, newBet);

                        foreach (var newOdd in newBet.Odds)
                        {
                            var existingOdd = existingBet.Odds.FirstOrDefault(o => o.ID == newOdd.ID);
                            if (existingOdd == null)
                            {
                                newOdd.BetID = existingBet.ID;
                                existingBet.Odds.Add(newOdd);
                            }
                            else
                            {
                                newOdd.BetID = existingOdd.BetID;
                                DetachAndUpdate(existingOdd, newOdd);
                            }
                        }
                    }
                }
            }

            _context.SaveChanges();
        }

        public Match GetMatch(string matchId)
        {
            return _context.Matches
            .Include(m => m.Bets)
            .ThenInclude(b => b.Odds)
            .FirstOrDefault(x => x.ID == matchId);
        }

        private void DetachAndUpdate<T>(T existingEntity, T newEntity) where T : BaseDataModel
        {
            _context.Entry(existingEntity).State = EntityState.Detached;
            _context.Entry(newEntity).State = EntityState.Modified;
        }
    }
}
