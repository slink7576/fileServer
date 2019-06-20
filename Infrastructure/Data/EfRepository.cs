using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly FilesContext _dbContext;

        public EfRepository(FilesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ListAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public IQueryable<T> ListAllQuery()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
