using GeoLinks.DataLayer.GenericRepository;
using GeoLinks.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.DataLayer.DalInterface
{
    public interface IUnitOfWork
    {
        IGenericRepository<ProfileDto> GenericProfileRepository { get; }
        IGenericRepository<UsersDto> GenericPasswordRepository { get; }
        IGenericRepository<LoginUser> GenericLogInUserRepository { get; }
        IGenericRepository<CartItemDto> GenericCartRepository { get; }
        IGenericRepository<ResetPasswordDto> GenericResetPasswordRepository { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}
