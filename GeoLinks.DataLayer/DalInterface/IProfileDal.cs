using GeoLinks.Entities.DbEntities;
using GeoLinks.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.DataLayer.DalInterface
{
    public interface IProfileDal
    {
        ProfileModal GetProfile(string userName);
        ProfileModal GetProfileById(int profileId);
    }
}
