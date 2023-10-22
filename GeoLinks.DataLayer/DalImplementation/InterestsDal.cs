using AutoMapper;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.DbEntities;
using GeoLinks.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoLinks.DataLayer.DalImplementation
{
    public class InterestsDal : IInterestsDal
    {
        private IUnitOfWork unitOfWork { get; set; }
        private MapperConfiguration mapperconfig;
        private MapperConfiguration mapperconfigdto;
        private IMapper mapper;
        private IMapper mapperDto;
        public InterestsDal(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            mapperconfig = AutoMapperConfig.CreateMapperConfig<InterestsModal, InterestsDto>();
            mapperconfigdto = AutoMapperConfig.CreateMapperConfig<InterestsDto, InterestsModal>();
            mapper = mapperconfig.CreateMapper();
            mapperDto = mapperconfigdto.CreateMapper();
        }
        public int AddInterest(InterestsModal interest)
        {
            InterestsDto intrdto = mapper.Map<InterestsModal, InterestsDto>(interest);
            this.unitOfWork.GenericInterestsRepository.Insert(intrdto);
            this.unitOfWork.Save();
            return intrdto.InterestId;
        }

        public List<InterestsModal> GetAllInterests()
        {
            List<InterestsModal> interestsModals = new List<InterestsModal>();
            var interests = this.unitOfWork.GenericInterestsRepository.GetAll().ToList();
            return mapperDto.Map<List<InterestsDto>, List<InterestsModal>>(interests);
        }
    }
}
