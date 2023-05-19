using AutoMapper;
using BlogApi.Dtos;
using BlogApi.Models;

namespace BlogApi.Profiles;

public class BlogApiProfile : Profile
{
    public BlogApiProfile()
    {
        CreateMap<Comment, CommentDto>();
        CreateMap<CommentDto, Comment>();
        CreateMap<Post, PostDto>();
        CreateMap<PostDto, Post>();
    }
}