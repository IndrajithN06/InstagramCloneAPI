// Controllers/FeedController.cs
using InstagramCloneAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class FeedController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FeedController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetHomeFeed()
    {
        var posts = await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new
            {
                p.Id,
                Username = p.User.Username,
                ProfilePictureUrl = p.User.ProfilePictureUrl,
                ImageUrl = p.ContentUrl,
                Caption = p.Caption,
                Likes = p.Likes.Count,
                Liked = false, // Set true based on current user if authenticated
                LikesCount = p.Likes.Count,
                Comments = p.Comments.Select(c => c.Text).ToList()
            })
            .ToListAsync();

        return Ok(posts);
    }

   

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost(int id)
    {
        var post = await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            return NotFound();

        return Ok(post);
    }

}
