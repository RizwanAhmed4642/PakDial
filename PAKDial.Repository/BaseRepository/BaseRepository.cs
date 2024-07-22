using Microsoft.EntityFrameworkCore;
using PAKDial.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.BaseRepository
{
    public abstract class BaseRepository<TDomainClass , TKeyType> : IBaseRepository<TDomainClass, TKeyType>
      where TDomainClass : class
    {
        #region Protected
        protected abstract DbSet<TDomainClass> DbSet { get; }
        #endregion

        #region Constructor
        protected BaseRepository(PAKDialSolutionsContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("container");
            }
            db = context;

        }
        #endregion

        #region Public
        public PAKDialSolutionsContext db;

        public virtual IQueryable<TDomainClass> Find(TDomainClass instance)
        {
            return DbSet.Find(instance) as IQueryable<TDomainClass>;
        }
        public virtual TDomainClass Find(TKeyType id)
        {
            return DbSet.Find(id);
        }
        public virtual IEnumerable<TDomainClass> GetAll()
        {
            return DbSet;
        }
        public virtual void Delete(TDomainClass instance)
        {
            if (db.Entry(instance).State == EntityState.Detached)
            {
                DbSet.Attach(instance);
            }
            DbSet.Remove(instance);
        }
        public virtual void Add(TDomainClass instance)
        {
            DbSet.Add(instance);
        }
        public virtual void Update(TDomainClass instance)
        {
            DbSet.Attach(instance);
            db.Entry(instance).State = EntityState.Modified;
        }

        public virtual void DeleteRange(List<TDomainClass> instance)
        {
            DbSet.RemoveRange(instance);
        }
        public virtual void AddRange(List<TDomainClass> instance)
        {
            DbSet.AddRange(instance);
        }
        public virtual void UpdateRange(List<TDomainClass> instance)
        {
            DbSet.UpdateRange(instance);
        }
        public virtual IEnumerable<TDomainClass> GetWithRawSql(string query,
        params object[] parameters)
        {
            return DbSet.FromSql(query, parameters).ToList();
        }

        public virtual int SaveChanges()
        {
            return db.SaveChanges();
        }

        

        #endregion

    }
}
