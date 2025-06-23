using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

public interface IPostService
{
    Task<PostDto> GetPostByIdAsync(int id);
    Task<IEnumerable<PostDto>> GetAllPostsAsync();
    Task<PostDto> CreatePostAsync(CreatePostDto createPostDto);
    Task<bool> UpdatePostAsync(int id, UpdatePostDto updatePostDto);
    Task<bool> DeletePostAsync(int id);
    Task<CommentDto> AddCommentAsync(int postId, CreateCommentDto commentDto);
    Task<bool> LikePostAsync(int postId, int userId);
    Task<bool> UnlikePostAsync(int postId, int userId);
    Task<IEnumerable<CommentDto>> GetCommentsAsync(int postId);
    Task<IEnumerable<LikeDto>> GetLikesAsync(int postId);
}

public class PostService : IPostService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PostService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PostDto> GetPostByIdAsync(int id)
    {
        var post = await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Comments).ThenInclude(c => c.User)
            .Include(p => p.Likes).ThenInclude(l => l.User)
            .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
            .FirstOrDefaultAsync(p => p.Id == id);

        return post == null ? null : _mapper.Map<PostDto>(post);
    }

    public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
    {
        var posts = await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Comments).ThenInclude(c => c.User)
            .Include(p => p.Likes).ThenInclude(l => l.User)
            .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
            .ToListAsync();

        return _mapper.Map<List<PostDto>>(posts);
    }

    public async Task<PostDto> CreatePostAsync(CreatePostDto createPostDto)
    {
        var post = _mapper.Map<Post>(createPostDto);
        post.CreatedAt = DateTime.UtcNow;

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return _mapper.Map<PostDto>(post);
    }

    public async Task<bool> UpdatePostAsync(int id, UpdatePostDto updatePostDto)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
            return false;

        _mapper.Map(updatePostDto, post);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePostAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
            return false;

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CommentDto> AddCommentAsync(int postId, CreateCommentDto commentDto)
    {
        var comment = _mapper.Map<Comment>(commentDto);
        comment.PostId = postId;
        comment.CreatedAt = DateTime.UtcNow;

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return _mapper.Map<CommentDto>(comment);
    }

    public async Task<bool> LikePostAsync(int postId, int userId)
    {
        var alreadyLiked = await _context.Likes
            .AnyAsync(l => l.PostId == postId && l.UserId == userId);

        if (alreadyLiked)
            return false;

        var like = new Like
        {
            PostId = postId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Likes.Add(like);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UnlikePostAsync(int postId, int userId)
    {
        var like = await _context.Likes
            .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);

        if (like == null)
            return false;

        _context.Likes.Remove(like);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsAsync(int postId)
    {
        var comments = await _context.Comments
            .Where(c => c.PostId == postId)
            .Include(c => c.User)
            .ToListAsync();

        return _mapper.Map<List<CommentDto>>(comments);
    }

    public async Task<IEnumerable<LikeDto>> GetLikesAsync(int postId)
    {
        var likes = await _context.Likes
            .Where(l => l.PostId == postId)
            .Include(l => l.User)
            .ToListAsync();

        return _mapper.Map<List<LikeDto>>(likes);
    }
}
