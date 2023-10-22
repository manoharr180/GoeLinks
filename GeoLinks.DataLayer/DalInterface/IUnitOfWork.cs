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
        IGenericRepository<Password> GenericPasswordRepository { get; }
        IGenericRepository<LoginUser> GenericLogInUserRepository { get; }
        IGenericRepository<FriendDto> GenericFriendsRepository { get; }
        IGenericRepository<FriendDetailDto> GenericFriendDetailsRepository { get; }
        IGenericRepository<HobbiesDto> GenericHobbiesRepository { get; }
        IGenericRepository<InterestsDto> GenericInterestsRepository { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}
