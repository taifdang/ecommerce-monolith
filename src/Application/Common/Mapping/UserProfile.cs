using AutoMapper;

namespace Application.Common.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        //CreateMap<UserUpdateRequest, ApplicationUser>().ForMember(x => x.ProductOptionId, y => y.Ignore());
    }
}
