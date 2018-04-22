using AdminPanel.Models;
using AdminPanel.ViewModels.Users;
using AutoMapper;

namespace AdminPanel.Services.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<tblUser, UserViewModel>();
            //CreateMap<tblClientFireDoorNote, ClientFireDoorNote>()
            //    .ForMember(x => x.AddedByName, y => y.MapFrom(src => src.AddedBy.FirstName + " " + src.AddedBy.LastName))
            //    .ForMember(x => x.InActivatedByName, y => y.MapFrom(src => src.InActivatedBy.FirstName + " " + src.InActivatedBy.LastName))
            //    .ForMember(x => x.DT_RowId, y => y.MapFrom(src => src.PKID.ToString()));
        }
    }
}
