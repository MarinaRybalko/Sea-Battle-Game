
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace GameDataLayer
{
    public class PlayersRepository : IRepository<BattlePlayer>
    {
        private PlayersDBEntities db;

        public PlayersRepository()
        {
            this.db = new PlayersDBEntities();
        }

        public IEnumerable GetTopTen()
        {
            var query = from p in db.BattlePlayer
                orderby p.Rating descending
                select new { p.Name, p.Rating };
            return query.Take(10).ToList();
        }

        public BattlePlayer GetPlayer(int id)
        {
            return db.BattlePlayer.Find(id);
        }

        public BattlePlayer GetPlayer(string name)
        {
            BattlePlayer newPlayer;
            var query = from player in db.BattlePlayer
                        where player.Name == name
                        select player;
            if(query.Count()==0)
            {
                return null;
            }
            else
            {
                newPlayer = new BattlePlayer();
                foreach (var q in query)
                {
                 
                    newPlayer.Id = q.Id;
                    newPlayer.Name = q.Name;
                    newPlayer.Rating = q.Rating;
                    newPlayer.DefeatAmount = q.DefeatAmount;
                    newPlayer.WinAmount = q.WinAmount;
                }

                return newPlayer;

                
            }
       
        }
        public void Create(BattlePlayer player)
        {
            db.BattlePlayer.Add(player);
        }
        public IQueryable<BattlePlayer> GetPlayers(string name)
        {
            var query = from player in db.BattlePlayer
                        where player.Name == name
                        select player;
            if (!query.Any())
            {
                return null;
            }
            else
            {
                return query;
            }
        }
        public void Update(BattlePlayer player)
        {            
            db.Entry(player).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            BattlePlayer book = db.BattlePlayer.Find(id);
            if (book != null)
                db.BattlePlayer.Remove(book);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<BattlePlayer> GetPlayersList()
        {
           return db.BattlePlayer.ToList();
        }

    }
}
