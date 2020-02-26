using AutoMapper;
using DDinst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace DDinst
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Код, выполняемый при запуске приложения
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutoMapper.Mapper.Initialize(cfg => {
                cfg.AddProfile<Base.BLL.DataProfile>();
                cfg.AddProfile<MapperProfile>();
            });
        }
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<Base.DTO.UserDTO, RegModel>()
                   // .ForMember(x => x.Birthdate, x => x.MapFrom(y => y.BirthDate))
                    .ReverseMap()
                   // .ForMember(x => x.BirtDate, x => x.MapFrom(y => y.Birtdate))
                    .ForMember(x => x.RegDate, x => x.MapFrom(y => DateTime.Now))
                    ;

                CreateMap<Base.DTO.UserDTO, Models.ProfileModel>()
                  //  .ForMember(x => x.Birtdate, x => x.MapFrom(y => y.Birth))
                    .ReverseMap()
                 //   .ForMember(x => x.Birth, x => x.MapFrom(y => y.Birtdate))
                    ;


            }
        }
    }
}