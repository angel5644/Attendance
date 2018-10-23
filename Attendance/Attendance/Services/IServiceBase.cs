using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Attendance.Services
{
    public interface IServiceBase<T> where T : class
    {
        /// <summary>
        /// Get an element by its id
        /// </summary>
        /// <param name="id">The id of the entity</param>
        /// <returns>The element matching the id</returns>
        Task<T> Get(int id);

        /// <summary>
        /// Get all elements
        /// </summary>
        /// <returns>All the elements</returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Updates an element
        /// </summary>
        /// <param name="entity">The element to be updated</param>
        /// <returns></returns>
        Task<int> Update(T entity);

        /// <summary>
        /// Creates an element 
        /// </summary>
        /// <param name="entity">The element to be created</param>
        /// <returns></returns>
        Task<int> Create(T entity);

        /// <summary>
        /// Removes an element by id
        /// </summary>
        /// <param name="id">The id of the element to remove</param>
        /// <returns></returns>
        Task<int> Delete(int id);

        /// <summary>
        /// Saves the changes 
        /// </summary>
        /// <returns></returns>
        Task<int> Save();

        void Dispose();
    }
}