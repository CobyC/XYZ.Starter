using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XYZ.Starter.Core.Interfaces.Classes;

namespace XYZ.Starter.Core.Interfaces.Data
{
    public interface IRepositoryBase
    {

    }
    public interface IRepositoryBase<T> : IRepositoryBase
        where T : class, IEntityBase
    {

        /// <summary>
        /// Use this to get the full entity structure, including sub entities.
        /// Intended for NotLazyLoading
        /// </summary>
        /// <param name="id">the id of the entity to fetch</param>        
        T FetchById(int id);

        /// <summary>
        /// Use this to get the full entity structure, including sub entities
        /// Intended for NotLazyLoading
        /// </summary>
        /// <param name="id">the id of the entity to fetch</param>        
        Task<T> FetchByIdAsync(int id);

        /// <summary>
        /// Adds a new entity to the underlying data store
        /// </summary>
        /// <param name="entity">the entity to store to the datastore, this should be a new entity that does not already exist</param>
        T Create(T entity);

        /// <summary>
        /// Adds a new entity to the underlying data store
        /// </summary>
        /// <param name="entity">the entity to store to the datastore, this should be a new entity that does not already exist</param>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Updates an existing entity with changed values
        /// </summary>
        /// <param name="entity">The entity to update</param>        
        T Update(T entity);

        /// <summary>
        /// Find the entity by its Id
        /// </summary>
        /// <param name="id">The id of the required entity</param>              
        T FindById(int id);

        /// <summary>
        /// Find the entity by its Id asynchronously
        /// </summary>
        /// <param name="id">The id of the required entity</param>        
        Task<T> FindByIdAsync(int id);

        /// <summary>
        /// Find entities by using a valid linq expression function.
        /// in most cases this could be seen as a Where(....).ToList() expression
        /// </summary>
        /// <param name="expression">the expression that will be used to query the datastore</param>
        /// <returns>IEnumerable of <typeparamref name="T"/></returns>
        IEnumerable<T> FindByExpression(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Find entities by using a valid linq expression function (asynchronously).
        /// in most cases this could be seen as a Where(....).ToListAsync() expression
        /// </summary>
        /// <param name="expression">the expression that will be used to query the datastore</param>
        /// <returns>IEnumerable of <typeparamref name="T"/></returns>
        Task<IEnumerable<T>> FindByExpressionAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Delete an entity from the underlying datastore
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(T entity);

        /// <summary>
        /// Commit all changes to the underlying data store synchronously
        /// </summary>        
        void SaveChanges();

        /// <summary>
        /// Commit all changes to the underlying data store asynchronously
        /// </summary>        
        Task SaveChangesAsync();
    }
}
