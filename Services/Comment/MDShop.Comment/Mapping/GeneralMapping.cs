using AutoMapper;
using MDShop.Comment.Dtos.CommentDtos;
using MDShop.Comment.Entities;

namespace MDShop.Comment.Mapping {
    public class GeneralMapping : Profile {
        public GeneralMapping() {

            CreateMap<UserComment, ResultCommentDto>().ReverseMap();
            CreateMap<UserComment, CreateCommentDto>().ReverseMap();
            CreateMap<UserComment, UpdateCommentDto>().ReverseMap();
            CreateMap<UserComment, GetByIdCommentDto>().ReverseMap();

        }
    }
}
