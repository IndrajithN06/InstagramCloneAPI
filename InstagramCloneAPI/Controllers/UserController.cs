using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using InstagramCloneAPI.DTOs;
using InstagramCloneAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;



namespace InstagramCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;

        public UserController(UserService userService, ICloudinaryService cloudinaryService,ApplicationDbContext context)
        {
            _userService = userService;
            _cloudinaryService = cloudinaryService;
            _context = context;
        }

        [HttpGet("{username}/followers")]
        [Authorize]
        public async Task<IActionResult> GetFollowers(string username)
        {
            var followers = await _userService.GetFollowersAsync(username);
            if (followers == null) return NotFound("User not found.");
            return Ok(followers);
        }

        [HttpGet("{username}/following")]
        [Authorize]
        public async Task<IActionResult> GetFollowing(string username)
        {
            var following = await _userService.GetFollowingAsync(username);
            if (following == null) return NotFound("User not found.");
            return Ok(following);
        }


        [HttpGet("{username}")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile(string username)
        {
            var user = await _userService.GetUserProfileAsync(username);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpGet("stats/{userId}")]
        public async Task<ActionResult<ProfileStatsDto>> GetProfileStats(int userId)
        {
            var followersCount = await _context.Follows.CountAsync(f => f.FollowingId == userId);
            var followingCount = await _context.Follows.CountAsync(f => f.FollowerId == userId);
            var postsCount = await _context.Posts.CountAsync(p => p.UserId == userId);

            var stats = new ProfileStatsDto
            {
                FollowersCount = followersCount,
                FollowingCount = followingCount,
                PostsCount = postsCount
            };

            return Ok(stats);
        }

        [HttpPut("update-profile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileRequest request)
        {
            string imageUrl = null;

            if (request.ProfilePicture != null)
            {
                imageUrl = await _cloudinaryService.UploadImageAsync(request.ProfilePicture, "profile_pics");
            }

            var updateDto = new UpdateProfileDto
            {
                Bio = request.Bio,
                ProfilePictureUrl = imageUrl
            };
            var userid = GetUserIdFromClaims();
            var updatedUser = await _userService.UpdateProfileAsync(userid,updateDto);

            return Ok(updatedUser);
        }


        private int GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // Look for the NameIdentifier claim
            if (userIdClaim != null)
                return int.Parse(userIdClaim.Value); // Parse the user ID from the claim

            return 0; // Return 0 if the claim is not found
        }
    }

   

}
