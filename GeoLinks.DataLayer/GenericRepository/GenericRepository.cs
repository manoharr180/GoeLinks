using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace GeoLinks.DataLayer.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private GeoLensContext geoLensContext;
        private DbSet<T> table;
        public GenericRepository(GeoLensContext geoLensContext)
        {
            this.geoLensContext = geoLensContext;
            table = geoLensContext.Set<T>();
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T Get(object id)
        {
            return table.Find(id);
        }

        public T Get(string sqlQuery, params object[] sqlParameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return table.AsQueryable();
        }

        public IEnumerable<T> GetAll(string sqlQuery,params object[] sqlParameters)
        {
            if (sqlParameters != null)
            {
                return table.FromSqlRaw(sqlQuery, sqlParameters);
            }
            else
            {
                return table.FromSqlRaw(sqlQuery);
            }
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            geoLensContext.SaveChanges();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            geoLensContext.Entry(obj).State = EntityState.Modified;

        }
    }
}
