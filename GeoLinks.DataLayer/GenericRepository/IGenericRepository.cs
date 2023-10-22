using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace GeoLinks.DataLayer.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(string sqlQuery, params object[] sqlParameters);
        T Get(object id);
        T Get(string sqlQuery, params object[] sqlParameters);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();

    }
}
