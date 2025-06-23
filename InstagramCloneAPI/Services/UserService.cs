using AutoMapper;
using InstagramCloneAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InstagramCloneAPI.Services
{
    public class UserService

    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserSummaryDto>> GetFollowersAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.Followers)
                    .ThenInclude(f => f.Follower)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null) return null;

            var followers = user.Followers.Select(f => f.Follower).ToList();
            return _mapper.Map<List<UserSummaryDto>>(followers);
        }

        public async Task<List<UserSummaryDto>> GetFollowingAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.Following)
                    .ThenInclude(f => f.Following)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null) return null;

            var following = user.Following.Select(f => f.Following).ToList();
            return _mapper.Map<List<UserSummaryDto>>(following);
        }

        public async Task<UserDto> GetUserProfileAsync(string username)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                Bio = user.Bio,
                ProfilePictureUrl = user.ProfilePictureUrl
            };
        }
        public async Task<bool> UpdateProfileAsync(int userId, UpdateProfileDto updateProfileDto)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return false;  // User not found
            }

            // Update bio and profile picture URL if provided
            user.Bio = updateProfileDto.Bio;
            user.ProfilePictureUrl = updateProfileDto.ProfilePictureUrl ?? user.ProfilePictureUrl;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }
        }


}
