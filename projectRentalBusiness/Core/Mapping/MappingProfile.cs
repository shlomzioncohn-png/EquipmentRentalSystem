using AutoMapper;
using Core.Models;
using Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {

            CreateMap<User, UserResource>();
            CreateMap<UserCreateResource, User>();
            CreateMap<UserResource, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Business, BusinessResource>()
                .ForMember(dest => dest.OwnerName,
                    opt => opt.MapFrom(src => src.UserOwner != null ? src.UserOwner.Name : null))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.UserOwner != null ? src.UserOwner.Email : null));

            CreateMap<BusinessResource, Business>()
                .ForMember(dest => dest.UserOwner, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<Item, ItemResource>()
                .ForMember(dest => dest.BusinessName,
                    opt => opt.MapFrom(src => src.AssociatedBusiness != null ? src.AssociatedBusiness.Name : null))
                .ForMember(dest => dest.BusinessCity,
                    opt => opt.MapFrom(src => src.AssociatedBusiness != null ? src.AssociatedBusiness.City : null));

            CreateMap<ItemResource, Item>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AssociatedBusiness, opt => opt.Ignore());

        }
    }
}
