using GeoLinks.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.Services.Services
{
    public interface IInterestsService
    {
        int AddInterest(InterestsModal interest);
        List<InterestsModal> GetAllInterests();
    }
}
