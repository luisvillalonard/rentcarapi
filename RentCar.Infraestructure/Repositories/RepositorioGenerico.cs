using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Diversos.Infraestructure.Contexto;
using Diversos.Core.Interfaces;
using Diversos.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Diversos.Core.Entidades;

namespace Diversos.Infraestructure.Repositories
{
    public class RepositorioGenerico<TEntity, TKey> : IRepositorioGenerico<TEntity, TKey>
        where TEntity : class
    {
        public readonly DiversosContext context;
        private IQueryable<TEntity> dbQuery;
        private ResponseResult result;

        public RepositorioGenerico(DiversosContext dbContext)
        {
            context = dbContext;
            dbQuery = context.Set<TEntity>().AsQueryable<TEntity>();
            result = new();
        }

        public virtual async Task<ResponseResult> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            return await GetAsync(null, null, orderBy, null);
        }

        public virtual async Task<ResponseResult> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetAsync(null, null, orderBy, includes);
        }

        public virtual async Task<ResponseResult> FindAsync(
            Expression<Func<TEntity, bool>> filters,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetAsync(null, filters, null, includes);
        }

        public virtual async Task<ResponseResult> FindAsync(
            RequestFilter requestFilter,
            Expression<Func<TEntity, bool>> filters,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetAsync(requestFilter, filters, null, includes);
        }

        public virtual async Task<ResponseResult> FindAsync(
            RequestFilter requestFilter,
            Expression<Func<TEntity, bool>> filters,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetAsync(requestFilter, filters, orderBy, includes);
        }

        private async Task<ResponseResult> GetAsync(
            RequestFilter requestFilter,
            Expression<Func<TEntity, bool>> filters,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includes)
        {
            if (dbQuery == null)
                return await Task.FromResult(new ResponseResult("Situación inesperada con el entorno de consulta."));

            result = new ResponseResult();

            try
            {
                // Agrego los filtros
                if (filters != null)
                    dbQuery = dbQuery.Where(filters);

                // Agrego las inclusiones
                if (includes != null)
                    foreach (Expression<Func<TEntity, object>> include in includes)
                        dbQuery = dbQuery.Include(include);

                // Agrego los filtros
                if (orderBy != null)
                    dbQuery = orderBy(dbQuery);

                // Obtengo todos los registros
                if (requestFilter != null)
                {

                    result.Datos = dbQuery
                        .AsNoTracking()
                        .AsEnumerable()
                        .Skip(requestFilter.CurrentPage - 1 * requestFilter.PageSize)
                        .Take(requestFilter.PageSize)
                        .ToArray();
                }
                else
                {
                    result.Datos = dbQuery
                        .AsNoTracking()
                        .AsEnumerable()
                        .ToArray();
                }

                if (requestFilter == null)
                    requestFilter = new();

                // Obtengo el total de registros de la entidad
                result.Paginacion = new()
                {
                    TotalRecords = dbQuery.AsNoTracking().Count(),
                    PageSize = requestFilter.PageSize,
                    CurrentPage = requestFilter.CurrentPage
                };
            }
            catch (Exception err)
            {
                return new ResponseResult($"Situación inesperada tratando de obtener los datos. {err.Message} {(err.InnerException ?? new Exception()).Message}".Trim(), false);
            }

            // Retorno el resultado
            return await Task.FromResult(result);
        }

        public virtual async Task<ResponseResult> GetByIdAsync(TKey id)
        {
            var item = await context.Set<TEntity>().FindAsync(id);
            if (item == null)
                return new ResponseResult("Código de entidad no encontrado.", false);

            return new ResponseResult(item);
        }

        public virtual async Task<ResponseResult> PostAsync(TEntity item)
        {
            try
            {
                context.Entry(item).State = EntityState.Added;
                await context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                return new ResponseResult($"Situación inesperada tratando de guardar los datos. {err.Message} {(err.InnerException ?? new Exception()).Message}".Trim(), false);
            }

            return new ResponseResult(item);
        }

        public async Task<ResponseResult> PostRangeAsync(TEntity[] items)
        {
            try
            {
                for (int pos = 0; pos < items.Length; pos++)
                {
                    context.Entry(items[pos]).State = EntityState.Added;
                    if ((pos % 10 == 0) || (pos == items.Length - 1))
                        await context.SaveChangesAsync();
                }
            }
            catch (Exception err)
            {
                return new ResponseResult($"Situación inesperada tratando de guardar los datos. {err.Message} {(err.InnerException ?? new Exception()).Message}".Trim(), false);
            }

            return new ResponseResult();
        }

        public virtual async Task<ResponseResult> PutAsync(TEntity item)
        {
            try
            {
                context.Entry(item).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                return new ResponseResult($"Situación inesperada tratando de actualizar los datos. {err.Message} {(err.InnerException ?? new Exception()).Message}".Trim(), false);
            }

            return new ResponseResult(item);
        }

        public async Task<ResponseResult> PutRangeAsync(TEntity[] items)
        {
            try
            {
                for (int pos = 0; pos < items.Length; pos++)
                {
                    context.Entry(items[pos]).State = EntityState.Modified;
                    if ((pos % 10 == 0) || (pos == items.Length - 1))
                        await context.SaveChangesAsync();
                }
            }
            catch (Exception err)
            {
                return new ResponseResult($"Situación inesperada tratando de actualizar los datos. {err.Message} {(err.InnerException ?? new Exception()).Message}".Trim(), false);
            }

            return new ResponseResult();
        }

        public virtual async Task<ResponseResult> ExecuteProcedureAsync(Procedimientos procedimiento, Dictionary<string, object> parametros)
        {
            try
            {
                string[] strParams = parametros.Select(x => x.Value != null ? $"{x.Key}={x.Value}" : $"{x.Key}=NULL").ToArray();
                                
                //var data = await Task.Run(() => context.Database.ExecuteSqlInterpolated($"EXEC {procedimiento.ToString()} {string.Join(",", strParams)}"));

                var dbSet = context.Set<TEntity>();
                var data = await Task.Run(() => dbSet.FromSqlRaw<TEntity>($"EXEC {procedimiento.ToString()} {string.Join(",", strParams)}").ToList());
                return new ResponseResult(data);
            }
            catch (Exception err)
            {
                return new ResponseResult($"Situación inesperada tratando de obtener los datos. {err.Message} {(err.InnerException ?? new Exception()).Message}".Trim(), false);
            }
        }
    }
}
