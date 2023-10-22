using GeoLinks.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.Services.Services
{
    public interface IFriendService
    {
        bool AddFriend(int FriendId, int UserId);
        List<FriendDetailsModal> GetFriends(int userId);
    }
}
