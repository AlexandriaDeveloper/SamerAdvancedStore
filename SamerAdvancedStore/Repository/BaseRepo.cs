using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using SamerAdvancedStore.Models;

namespace SamerAdvancedStore.Repository
{
    public abstract class BaseRepo<T> where T : class
    {
        private AppContext db = null;
        private DbSet<T> Entity = null;

        public BaseRepo(AppContext context)
        {
            db = context;
            Entity = db.Set<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = Entity;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = Entity;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
   
        public virtual T GetByID(object id)
        {
            return Entity.Find(id);
        }
        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await Entity.FindAsync(id);
        }
        public virtual  T GetSingle(Expression<Func<T, bool>> filter = null)
        {
            return  Entity.SingleOrDefault(filter);
        }
        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter = null)
        {

            return await Entity.SingleOrDefaultAsync(filter);
        }
        public virtual void Insert(T entity)
        {
            Entity.Add(entity);

        }


        public virtual void InsertRange(List<T> entity)
        {
            Entity.AddRange(entity);

        }
        public virtual void Delete(object id)
        {
            T entityToDelete = Entity.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (db.Entry(entityToDelete).State == EntityState.Detached)
            {
                Entity.Attach(entityToDelete);
            }
            Entity.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            Entity.Attach(entityToUpdate);
            db.Entry(entityToUpdate).State = EntityState.Modified;
        }



        public virtual bool Exist(IEnumerable<T> list)
        {
            return list.Count()>0?true:false;
        }
       

        public virtual int Count(IEnumerable<T> list)
        {
            return list.Count() ;
        }
    }
}