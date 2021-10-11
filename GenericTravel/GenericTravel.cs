using System;
using System.Collections.Generic;
using System.Linq;
using Entity_Model_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace GenericTravel
{
    public class GenericTravel<T> : ITravel<T> where T : class
    {
        private TravelPortalContext db;
        private DbSet<T> table = null;
        public GenericTravel(TravelPortalContext d)
        {
            db = d;
            table = db.Set<T>();
        }

        public T add(T data)
        {
            table.Add(data);
            db.SaveChanges();
            return data;

        }

        public IEnumerable<T> Getall()
        {
            return table.ToList();
        }

        public T getbyid(object id)
        {
            var data = db.Find<T>(id);
            return data;
        }

        public T update(T data)
        {
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
            return data;
        }


    }

    public interface ITravel<T> where T : class
    {
    }
}

