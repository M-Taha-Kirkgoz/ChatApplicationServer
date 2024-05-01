using ChatApplicationServer.Context;
using ChatApplicationServer.Dtos;
using ChatApplicationServer.Models;
using GenericFileService.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApplicationServer.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]

public sealed class AuthController(
    ApplicationDbContext context) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto request, CancellationToken cancellationToken)
    {
        // IActionResult == Result yani geri dönüş tipi belirtmek için kullanılır (Json - Status - Redirect)
        // CancellationToke == Bir görevin veya işlemin iptal edilmesine izin verir.

        bool isNameExists = await context.Users.AnyAsync(p => p.Name == request.Name, cancellationToken);

        if (isNameExists)
            return BadRequest(new { Message = "Bu kullanıcı adı daha önce kullanılmış!" });

        string avatar = FileService.FileSaveToServer(request.File, "wwwroot/avatar/");

        User user = new User()
        {
            Name = request.Name,
            Avatar = avatar,
        };

        await context.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync();
        
        return NoContent();
    }
}
