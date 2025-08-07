using GeoLinks.DataLayer.DalInterface;
using GeoLinks.DataLayer.GenericRepository;
using GeoLinks.Entities.DbEntities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;

namespace GeoLinks.DataLayer.DalImplementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        //Here TContext is nothing but your DBContext class
        //In our example it is EmployeeDBContext class
        //private readonly TContext _context;
        private bool _disposed;
        private string _errorMessage = string.Empty;
        private IDbContextTransaction _objTran;
        private Dictionary<string, object> _repositories;
        private IGenericRepository<ProfileDto> genericProfileRepository;
        private IGenericRepository<UsersDto> genericPasswordRepository;
        private IGenericRepository<LoginUser> genericLogInRepository;
        private GeoLensContext geoLensContext;
        //Using the Constructor we are initializing the _context variable is nothing but
        //we are storing the DBContext (EmployeeDBContext) object in _context variable
        public UnitOfWork(GeoLensContext geoLensContext)
        {
            this.geoLensContext = geoLensContext;
        }
        //The Dispose() method is used to free unmanaged resources like files, 
        //database connections etc. at any time.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        //This Context property will return the DBContext object i.e. (EmployeeDBContext) object

        public IGenericRepository<ProfileDto> GenericProfileRepository
        {
            get
            {
                if (this.genericProfileRepository == null)
                    this.genericProfileRepository = new GenericRepository<ProfileDto>(this.geoLensContext);
                return genericProfileRepository;
            }
        }

        public IGenericRepository<UsersDto> GenericPasswordRepository
        {
            get
            {
                if (this.genericPasswordRepository == null)
                    this.genericPasswordRepository = new GenericRepository<UsersDto>(this.geoLensContext);
                return genericPasswordRepository;
            }
        }

        public IGenericRepository<LoginUser> GenericLogInUserRepository
        {
            get
            {
                if (this.genericLogInRepository == null)
                    this.genericLogInRepository = new GenericRepository<LoginUser>(this.geoLensContext);
                return genericLogInRepository;
            }
        }

        //This CreateTransaction() method will create a database Trnasaction so that we can do database operations by
        //applying do evrything and do nothing principle
        public void CreateTransaction()
        {
            _objTran = this.geoLensContext.Database.BeginTransaction();
        }
        //If all the Transactions are completed successfuly then we need to call this Commit() 
        //method to Save the changes permanently in the database
        public void Commit()
        {
            _objTran.Commit();
        }
        //If atleast one of the Transaction is Failed then we need to call this Rollback() 
        //method to Rollback the database changes to its previous state
        public void Rollback()
        {
            _objTran.Rollback();
            _objTran.Dispose();
        }
        //This Save() Method Implement DbContext Class SaveChanges method so whenever we do a transaction we need to
        //call this Save() method so that it will make the changes in the database
        public void Save()
        {
            try
            {
                this.geoLensContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                throw new Exception(_errorMessage, dbEx);
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    this.geoLensContext.Dispose();
            _disposed = true;
        }
        public GenericRepository<T> GenericRepository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, object>();
            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<T>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), this.geoLensContext);
                _repositories.Add(type, repositoryInstance);
            }
            return (GenericRepository<T>)_repositories[type];
        }
    }
}
