using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.Services.Implementations
{
    public class FriendService : IFriendService
    {
        private IFriendDal friendDal;
        public FriendService(IFriendDal friendDal)
        {
            this.friendDal = friendDal;
        }
        public bool AddFriend(int FriendId, int UserId)
        {
            return this.friendDal.AddFriend(FriendId, UserId);
        }

        public List<FriendDetailsModal> GetFriends(int userId)
        {
            return this.friendDal.GetFriends(userId);
        }
    }
}
