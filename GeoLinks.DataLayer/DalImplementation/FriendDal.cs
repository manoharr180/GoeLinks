using AutoMapper;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.DbEntities;
using GeoLinks.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GeoLinks.DataLayer.DalImplementation
{
    public class FriendDal : IFriendDal
    {
        private IUnitOfWork unitOfWork { get; set; }
        private MapperConfiguration mapperconfig;
        private MapperConfiguration mapperconfigFrnds;
        private IMapper mapper;
        public FriendDal(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            mapperconfig = AutoMapperConfig.CreateMapperConfig<FriendDto, FriendModal>();
            mapperconfigFrnds = AutoMapperConfig.CreateMapperConfig<FriendDetailDto, FriendDetailsModal>();
            mapper = mapperconfig.CreateMapper();
            mapper = mapperconfigFrnds.CreateMapper();
        }
        public bool AddFriend(int FriendId, int UserId)
        {
            this.unitOfWork.GenericFriendsRepository.Insert(new FriendDto()
            {
                CreatedOn = DateTime.Now,
                FriendProfileId = FriendId,
                IsFriend = true,
                IsProfileBlocked = false,
                ModifiedOn = DateTime.Now,
                ProfileId = UserId
            });
            this.unitOfWork.GenericFriendsRepository.Save();
            return true;
        }

        public List<FriendDetailsModal> GetFriends(int userId)
        {
            List<FriendDetailsModal> friends = new List<FriendDetailsModal>();
            SqlParameter paras = new SqlParameter("userId", userId);

            var frnds = this.unitOfWork.GenericFriendDetailsRepository.GetAll(
                "select fl.FriendProfileId, pd.FName, pd.LName, pd.UserName, pd.BloodGroup, fl.CreatedOn as FrndAddedOn" +
                " from FriendsList_Tbl fl inner join Profile_Details pd on  pd.ProfileId = fl.FriendProfileId" +
                "  where fl.ProfileId = {0} and fl.IsFriend = 1 and fl.IsProfileBlocked = 0 and pd.IsActive = 1", userId).ToList();
            friends = mapper.Map<List<FriendDetailDto>, List<FriendDetailsModal>>(frnds);
            return friends;
        }
    }
}
