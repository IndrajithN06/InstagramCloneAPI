using Microsoft.AspNetCore.Mvc;

public class UpdateProfileRequest
{
    [FromForm]
    public string Bio { get; set; }

    [FromForm]
    public IFormFile ProfilePicture { get; set; }
}
