using GeoLinks.Entities.DbEntities;
using GeoLinks.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.DataLayer.DalInterface
{
    public interface IFriendDal
    {
        bool AddFriend(int FriendId , int UserId);
        List<FriendDetailsModal> GetFriends(int userId);
    }
}
