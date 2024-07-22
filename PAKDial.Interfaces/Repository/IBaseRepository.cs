using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IBaseRepository<TDomainClass, TKeyType>
        where TDomainClass : class
    {
        void Update(TDomainClass instance);
        void Delete(TDomainClass instance);
        void Add(TDomainClass instance);
        void UpdateRange(List<TDomainClass> instance);
        void DeleteRange(List<TDomainClass> instance);
        void AddRange(List<TDomainClass> instance);
        int SaveChanges();
        IQueryable<TDomainClass> Find(TDomainClass instance);
        TDomainClass Find(TKeyType id);
        IEnumerable<TDomainClass> GetAll();
        IEnumerable<TDomainClass> GetWithRawSql(string query,
        params object[] parameters);
    }
}
