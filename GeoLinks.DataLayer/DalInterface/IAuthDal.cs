using GeoLinks.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.DataLayer.DalInterface
{
    public interface IAuthDal
    {
        int RegisterUser(ProfileModal profileModal);
        bool ValidateUser(string userMail, string phoneNum, string password);
    }
}
