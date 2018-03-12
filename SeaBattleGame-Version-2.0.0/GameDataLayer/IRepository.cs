using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace GameDataLayer
{
    /// <summary>
    /// Provides a mechanism for working with database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IDisposable
        where T : class
    {
        /// <summary>
        /// Gets all records from database
        /// </summary>
        /// <returns>List of records </returns>
        IEnumerable<T> GetPlayersList();
        /// <summary>
        /// Gets ten records from database
        /// </summary>
        /// <returns>Top ten best players</returns>
        IEnumerable GetTopTen();
        /// <summary>
        /// Gets record from database by id attribute
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Player with the given id</returns>
        T GetPlayer(int id); 
        /// <summary>
        /// Create a new record in database
        /// </summary>
        /// <param name="item"></param>
        void Create(T item); 
        /// <summary>
        /// Uptate existing record in database
        /// </summary>
        /// <param name="item"></param>
        void Update(T item);
        /// <summary>
        /// Delete record from database by id attribute
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id); 
        /// <summary>
        /// Save all shanges into database
        /// </summary>
        void Save();
        /// <summary>
        /// Gets record from database by name attribute
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Saved records </returns>
        T GetPlayer(string name);
        /// <summary>
        /// Gets record from database by name attribute
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Records with the given name attribute</returns>
        IQueryable<T> GetPlayers(string name);

    }
}
