using System;
using System.Linq;
using AutoMapper;
using DDinst.Base.DAL.Entity;
using DDinst.Base.DTO;

namespace DDinst.Base.BLL
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<User, UserDTO>()
                .ReverseMap()
                .ForMember(x => x.RegDate, x => x.Ignore())
                .ForMember(x => x.PassHash, x => x.Ignore())
                .ForMember(x => x.Salt, x => x.Ignore())
                ;

            //CreateMap<DAL.Entity.Image, DTO.ImageDTO>()
            //    .ReverseMap()
       
            //    ;

            //CreateMap<DAL.Entity.Post, DTO.PostDTO>()
            //    .ForMember(x => x.PostImages, x => x.MapFrom(y => y.PostImages.OrderBy(q => q.OrderNum).Select(q => q.Image.Id)))
            //    .ForMember(x => x.Owner, x => x.MapFrom(y => y.User.Nickname))
            //    .ReverseMap()
            //    .ForMember(x => x.DateCreated, x => x.MapFrom(y => DateTime.Now))
            //    .ForMember(x => x.User, x => x.Ignore())
            //    .ForMember(x => x.PostImages, x => x.Ignore())

            //    ;

        }
    }
}