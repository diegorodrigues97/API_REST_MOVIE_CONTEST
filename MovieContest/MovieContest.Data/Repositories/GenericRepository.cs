using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieContest.Domain;
using MovieContest.Domain.Entities;

namespace MovieContest.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dataset;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public T Create(T entity)
        {
            try
            {
                _dataset.Add(entity);
                _context.SaveChanges();

                return entity;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.Message);
            }
        }
        public T Update(T entity)
        {
            var target = _dataset.SingleOrDefault(p => p.Id.Equals(entity.Id));

            if (target == null)
                return null;

            try
            {
                _context.Entry(target).CurrentValues.SetValues(entity);
                _context.SaveChanges();

                return target;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.Message);
            }
        }

        public void Delete(long id)
        {
            var entity = _dataset.SingleOrDefault(p => p.Id.Equals(id));

            if (entity == null) return;

            try
            {
                _dataset.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.Message);
            }
        }


        public List<T> FindAll()
        {
            try
            {
                return _dataset.ToList();
            }
            catch(Exception e)
            {
                throw new DatabaseException(e.Message);
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                if (_dataset.Any())
                    return _dataset.ToList();

                return new List<T>();
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.Message);
            }
        }

        public T FindById(long id)
        {
            try
            {
                return _dataset.SingleOrDefault(p => p.Id.Equals(id));
            }
            catch(Exception e)
            {
                throw new DatabaseException(e.Message);
            }
        }

        public bool Exists(long id)
        {
            try
            {
                return _dataset.Any(p => p.Id.Equals(id));
            }
            catch(Exception e)
            {
                throw new DatabaseException(e.Message);
            }
        }
    }
}
