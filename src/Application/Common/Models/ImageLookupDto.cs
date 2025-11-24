using AutoMapper;
using Domain.Entities;

namespace Application.Common.Models;

public class ImageLookupDto 
{ 
    public Guid Id { get; set; }
    public string? Url { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductImage, ImageLookupDto>()
                .ForMember(d => d.Url, opt => opt.MapFrom(src => src.ImageUrl));
        }
    }
}