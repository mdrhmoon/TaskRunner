using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskRunner.Data
{
    public class GenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        public DbSet<T> table;

        public GenericRepository(DbContext context)
        {
            this._context = context;
            table = _context.Set<T>();
        }

        // For Insertion
        public async Task<T> Insert(T entity)
        {
            await table.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        // For Update
        public async Task<T> Update(T entity, Expression<Func<T, bool>> predicate)
        {
            T value = await table.Where(predicate).FirstOrDefaultAsync();
            _context.Entry(value).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        // For Delete
        public async Task<Boolean> Delete(Expression<Func<T, bool>> predicate)
        {
            T value = await table.Where(predicate).FirstOrDefaultAsync();
            _context.Remove(value);
            await _context.SaveChangesAsync();

            return true;
        }

        // Get All Rows from data base
        public async Task<List<T>> GetAll()
        {
            return await table.ToListAsync();
        }


        // find by condition
        public async Task<List<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await table.Where(predicate).ToListAsync();
        }

        // find one by condition
        public async Task<T> FindOne(Expression<Func<T, bool>> predicate)
        {
            return await table.Where(predicate).FirstOrDefaultAsync();
        }

        // get Data Using query
        public async Task<List<T>> FindUsingRawQuery(string query, SqlParameter[] param)
        {
            if(param.Any()) return await _context.Set<T>().FromSqlRaw(query, param).ToListAsync();

            return await _context.Set<T>().FromSqlRaw(query).ToListAsync();
        }

        // get Data Using query
        public async Task<T> FindOneUsingRawQuery(string query, SqlParameter[] param)
        {
            if (param.Any()) return await _context.Set<T>().FromSqlRaw(query, param).FirstOrDefaultAsync();

            return await _context.Set<T>().FromSqlRaw(query).FirstOrDefaultAsync();
        }
    }
}