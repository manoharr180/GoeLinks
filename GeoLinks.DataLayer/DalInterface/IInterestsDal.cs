using GeoLinks.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.DataLayer.DalInterface
{
    public interface IInterestsDal
    {
        int AddInterest(InterestsModal interest);
        List<InterestsModal> GetAllInterests();
    }
}
