using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Models
{
    public interface IBowlersRepository
    {
        IQueryable<Team> Teams { get; }
        IQueryable<Bowler> Bowlers { get; }
        public void SaveBowler(Bowler b);
        public void CreateBowler(Bowler b);
        public void DeleteBowler(Bowler b);
        public void UpdateBowler(Bowler b);
    }
}
