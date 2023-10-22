using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.DataLayer
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration CreateMapperConfig<S, D>()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<S, D>();
                cfg.CreateMap<D, S>();
            });
            return config;
        }
    }
}
