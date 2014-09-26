using SimpleMVCBlog.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SimpleMVCBlog.Web.Repositories
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindAllBy(Expression<Func<T, bool>> predicate);
        T FindFirstBy(Expression<Func<T, bool>> predicate);
        T Find(int id);
        void Add(T entity);
        void Delete(T entity);
        void Delete(IQueryable<T> entities);
        void Edit(T entity);
        int Save();
        int Count();
        void Refresh(T entity);
    }

    public abstract class GenericRepository<T> :
     IGenericRepository<T>
     where T : class
    {

        //protected static ILog log;
        public AppDbContext Context{ get; set; }

        public GenericRepository()//IDbContextFactory dbContextFactory)
        {
            Context = new AppDbContext();//dbContextFactory.GetDbContext();
            //log = LogManager.GetLogger(GetType());
        }
        public virtual IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public virtual IQueryable<T> FindAllBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public virtual T FindFirstBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().FirstOrDefault(predicate);
        }

        public virtual void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual void Delete(IQueryable<T> entities)
        {
            foreach (T entity in entities.ToList())
                Context.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual int Save()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        //log.Error(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                //log.Error("Save failed", ex);
            }
            return 0;
        }

        public virtual T Find(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Refresh(T entity)
        {
            Context.Entry(entity).Reload();
        }

        public virtual int Count()
        {
            return Context.Set<T>().Count();
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
        }
    }
}