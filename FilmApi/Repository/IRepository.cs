using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FilmApi.Models;

namespace FilmApi.Repository
{
     public interface IRepository<T> where T : IModel, new()
     {
          T Insert(T item);
          void Update(int id, T item);
          T Get(int id);
          void Delete(int id);
          IEnumerable<T> Find(Func<T, bool> find);
          IEnumerable<T> List();
     }
}