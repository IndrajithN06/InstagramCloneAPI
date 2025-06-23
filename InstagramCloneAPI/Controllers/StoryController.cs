using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StoryController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public StoryController(ApplicationDbContext context, ICloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    // POST: api/Story
    [HttpPost]
    public async Task<IActionResult> CreateStory(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is required.");

        // Upload to Cloudinary
        var videoUrl = await _cloudinaryService.UploadVideoAsync(file);
        if (videoUrl == null)
            return StatusCode(500, "Video upload failed.");

        // Get UserId from claims
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized("User ID not found in token.");

        int userId = int.Parse(userIdClaim.Value);

        var story = new Story
        {
            UserId = userId,
            ContentUrl = videoUrl,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };

        _context.Stories.Add(story);
        await _context.SaveChangesAsync();

        return Ok(new { story.Id, story.ContentUrl });
    }

    // GET: api/Story
    [HttpGet]
    public async Task<IActionResult> GetActiveStories()
    {
        var currentTime = DateTime.UtcNow;

        var stories = await _context.Stories
            //.Where(s => s.ExpiresAt > currentTime)
            .Include(s => s.User)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();

        return Ok(stories);
    }
}
