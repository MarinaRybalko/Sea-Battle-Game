using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataLayer
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetPlayersList();
        IEnumerable GetTopTen();
        T GetPlayer(int id); 
        void Create(T item); 
        void Update(T item);
        void Delete(int id); 
        void Save();
        T GetPlayer(string name);
        IQueryable<T> GetPlayers(string name);

    }
}
