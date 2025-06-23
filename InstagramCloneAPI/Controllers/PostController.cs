using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    // GET: api/posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetAllPosts()
    {
        var posts = await _postService.GetAllPostsAsync();
        return Ok(posts);
    }

    // GET: api/posts/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetPost(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
            return NotFound();

        return Ok(post);
    }

    // POST: api/posts
    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        var post = await _postService.CreatePostAsync(createPostDto);
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    // PUT: api/posts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostDto updatePostDto)
    {
        var updated = await _postService.UpdatePostAsync(id, updatePostDto);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/posts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var deleted = await _postService.DeletePostAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    // POST: api/posts/{postId}/comments
    [HttpPost("{postId}/comments")]
    public async Task<ActionResult<CommentDto>> AddComment(int postId, [FromBody] CreateCommentDto commentDto)
    {
        var comment = await _postService.AddCommentAsync(postId, commentDto);
        return Ok(comment);
    }

    // GET: api/posts/{postId}/comments
    [HttpGet("{postId}/comments")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments(int postId)
    {
        var comments = await _postService.GetCommentsAsync(postId);
        return Ok(comments);
    }

    // POST: api/posts/{postId}/like?userId={userId}
    [HttpPost("{postId}/like")]
    public async Task<IActionResult> LikePost(int postId, [FromQuery] int userId)
    {
        var liked = await _postService.LikePostAsync(postId, userId);
        if (!liked)
            return BadRequest("Already liked");

        return Ok("Post liked");
    }

    // DELETE: api/posts/{postId}/like?userId={userId}
    [HttpDelete("{postId}/like")]
    public async Task<IActionResult> UnlikePost(int postId, [FromQuery] int userId)
    {
        var unliked = await _postService.UnlikePostAsync(postId, userId);
        if (!unliked)
            return NotFound("Like not found");

        return Ok("Post unliked");
    }

    // GET: api/posts/{postId}/likes
    [HttpGet("{postId}/likes")]
    public async Task<ActionResult<IEnumerable<LikeDto>>> GetLikes(int postId)
    {
        var likes = await _postService.GetLikesAsync(postId);
        return Ok(likes);
    }
}
