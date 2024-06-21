using Diversos.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Diversos.Core.Interfaces
{
    public interface IRepositorioGenerico<TEntity, TKey> where TEntity : class
    {
        Task<ResponseResult> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy
        );

        Task<ResponseResult> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includes
        );

        Task<ResponseResult> FindAsync(
            Expression<Func<TEntity, bool>> filters,
            params Expression<Func<TEntity, object>>[] includes
        );

        Task<ResponseResult> FindAsync(
            RequestFilter requestFilter,
            Expression<Func<TEntity, bool>> filters,
            params Expression<Func<TEntity, object>>[] includes
        );

        Task<ResponseResult> FindAsync(
            RequestFilter requestFilter,
            Expression<Func<TEntity, bool>> filters,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includes
        );
        
        Task<ResponseResult> GetByIdAsync(TKey id);

        Task<ResponseResult> PostAsync(TEntity item);

        Task<ResponseResult> PostRangeAsync(TEntity[] items);

        Task<ResponseResult> PutAsync(TEntity item);

        Task<ResponseResult> PutRangeAsync(TEntity[] items);

        Task<ResponseResult> ExecuteProcedureAsync(
            Procedimientos procedimiento, 
            Dictionary<string, object> parametros
        );
    }
}
