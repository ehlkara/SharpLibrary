using AutoMapper;
using SharpLibrary.Models.Entities;

namespace SharpLibrary.BusinessLayer.Mapping
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<Book, BookRequest>().ReverseMap();
            CreateMap<Book, BookResponse>().ReverseMap();
            CreateMap<Book, UpdateBookRequest>().ReverseMap();
        }
    }

}

