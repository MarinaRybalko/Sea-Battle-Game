
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
        /// <summary>
        /// Initialize a new instance of <see cref="PlayersRepository" /> class
        /// </summary>
        public PlayersRepository()
        {
            db = new PlayersDBEntities();
        }
        /// <summary>
        /// Gets top ten players from database
        /// </summary>
        /// <returns>Top ten best players</returns>
        public IEnumerable GetTopTen()
        {
          
            var query = from p in db.BattlePlayer
                orderby p.Rating descending
                select new { p.Name, p.Rating };
        
            return query.Take(10).ToList();
        }
        /// <summary>
        /// Gets player from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Player with the given id</returns>
        public BattlePlayer GetPlayer(int id)
        {
           
          
            return db.BattlePlayer.Find(id);
        }
        /// <summary>
        /// Gets player from database by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Saved records </returns>
        public BattlePlayer GetPlayer(string name)
        {
          
            var query = from player in db.BattlePlayer
                        where player.Name == name
                        select player;
            if(!query.Any())
            {
               
                return null;
            }
            else
            {
               
                var newPlayer = new BattlePlayer();
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
        /// <summary>
        /// Create a new player in database
        /// </summary>
        /// <param name="player"></param>
        public void Create(BattlePlayer player)
        {
            
            db.BattlePlayer.Add(player);
            
        }
        /// <summary>
        /// Gets player from database by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Records with the given name attribute</returns>
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
        /// <summary>
        /// Uptate information about existing player in database
        /// </summary>
        /// <param name="player"></param>
        public void Update(BattlePlayer player)
        {
           
            db.Entry(player).State = EntityState.Modified;
           
        }
        /// <summary>
        /// Delete player from database by id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
         
            BattlePlayer player = db.BattlePlayer.Find(id);
            if (player != null)
            {
                
                db.BattlePlayer.Remove(player);
               
            }
            
        }
        /// <summary>
        /// Save all shanges into database
        /// </summary>
        public void Save()
        {
          
            db.SaveChanges();
       
        }
         
        private bool _disposed;
        /// <summary>
        /// Release unmanaged resources
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
           
          
            if (!_disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            _disposed = true;
         
        }
        /// <summary>
        /// Release unmanaged resources
        /// </summary>
        public void Dispose()
        {
         
            Dispose(true);
            GC.SuppressFinalize(this);
            
        }
        /// <summary>
        /// Gets list of players from database
        /// </summary>
        /// <returns>List of players </returns>
        public IEnumerable<BattlePlayer> GetPlayersList()
        {

            return db.BattlePlayer.ToList();
        }

    }
}
