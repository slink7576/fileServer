using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IEnumerable<T> ListAll();
        IQueryable<T> ListAllQuery();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
