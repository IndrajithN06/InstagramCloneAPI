using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Entity to DTO
        CreateMap<User, UserDto>();
        CreateMap<Comment, CommentDto>();
        CreateMap<Like, LikeDto>();
        CreateMap<Tag, TagDto>();
        CreateMap<Message, MessageDto>();

        CreateMap<Post, PostDto>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                src.PostTags.Select(pt => pt.Tag)));

        // DTO to Entity
        CreateMap<CreatePostDto, Post>()
            .ForMember(dest => dest.PostTags, opt => opt.Ignore()); // handled manually

        CreateMap<CreateMessageDto, Message>();


        CreateMap<UpdatePostDto, Post>()
            .ForMember(dest => dest.PostTags, opt => opt.Ignore()); // handled manually

        CreateMap<CreateCommentDto, Comment>();
        CreateMap<User, UserSummaryDto>();

    }
}
