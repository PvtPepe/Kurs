using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using ClinicAppDAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ClinicAppDAL.Models.Base;

namespace ClinicAppDAL.Repos
{
    public class BaseRepo<T> : IDisposable, IRepo<T> where T:EntityBase, new()
    {
        private readonly DbSet<T> _table;
        private readonly DbContext _db;
        protected DbContext Context => _db;

        public BaseRepo(DbContext context)
        {
            _db = context;
            _table = _db.Set<T>();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }

        public int Add(IList<T> entities)
        {
            _table.AddRange(entities);
            return SaveChanges();
        }

        public int Update(T entity)
        {
            _table.Update(entity);
            return SaveChanges();
        }

        public int Update(IList<T> entities)
        {
            _table.UpdateRange(entities);
            return SaveChanges();
        }

        public int Delete(int id, byte[] timestamp)
        {
            _db.Entry(new T()
            { Id = id, Timestamp = timestamp }).State = EntityState.Deleted;
            return SaveChanges();
        }

        public int Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public T GetOne(int? id)
        {
            return _table.Find(id);
        }

        public List<T> GetSome(Expression<Func<T, bool>> where)
        {
            return _table.Where(where).ToList();
        }

        public List<T> GetAll()
        {
            return _table.ToList();
        }

        public List<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending)
        {
            return (ascending ? _table.OrderBy(orderBy) : _table.OrderByDescending(orderBy)).ToList();
        }

        public List<T> ExecuteQuery(string sql)
        {
            return _table.FromSqlRaw(sql).ToList();
        }

        public List<T> ExecuteQuery(string sql, object[] sqlParametersObjects)
        {
            return _table.FromSqlRaw(sql, sqlParametersObjects).ToList();
        }
        
        internal int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch(RetryLimitExceededException ex)
            {
                throw;
            }
            catch(DbUpdateException ex)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
