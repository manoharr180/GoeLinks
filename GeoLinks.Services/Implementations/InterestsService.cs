using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.Services.Implementations
{
    public class InterestsService : IInterestsService
    {
        private IInterestsDal interestsDal;
        public InterestsService(IInterestsDal interestsDal)
        {
            this.interestsDal = interestsDal;
        }
        public int AddInterest(InterestsModal interest)
        {
            return this.interestsDal.AddInterest(interest);
        }

        public List<InterestsModal> GetAllInterests()
        {
            return this.interestsDal.GetAllInterests();
        }
    }
}
