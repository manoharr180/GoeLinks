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
    public class HobbiesDal : IHobbiesDal
    {
        private IUnitOfWork unitOfWork { get; set; }
        private MapperConfiguration mapperconfig;
        private MapperConfiguration mapperconfigdto;
        private IMapper mapper;
        private IMapper mapperDto;

        public HobbiesDal(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            mapperconfig = AutoMapperConfig.CreateMapperConfig<HobbiesModal, HobbiesDto>();
            mapperconfigdto = AutoMapperConfig.CreateMapperConfig<HobbiesDto, HobbiesModal>();
            mapper = mapperconfig.CreateMapper();
            mapperDto = mapperconfigdto.CreateMapper();
        }
        public int AddHobby(HobbiesModal hobby)
        {
            HobbiesDto hobb = mapper.Map<HobbiesModal, HobbiesDto>(hobby);
            this.unitOfWork.GenericHobbiesRepository.Insert(hobb);
            this.unitOfWork.Save();
            return hobb.HobbiesId;
        }

        public List<HobbiesModal> GetAllHobbies()
        {
            List<HobbiesModal> hobbiesModals = new List<HobbiesModal>();
            var hobbies = this.unitOfWork.GenericHobbiesRepository.GetAll().ToList();
            return mapperDto.Map<List<HobbiesDto>, List<HobbiesModal>>(hobbies);
        }
    }
}
