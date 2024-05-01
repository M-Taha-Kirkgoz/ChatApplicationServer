using ChatApplicationServer.Context;
using ChatApplicationServer.Dtos;
using ChatApplicationServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApplicationServer.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]

public sealed class ChatsController(
    ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetChats(Guid userId, Guid toUserId, CancellationToken cancellationToken)
    {
        List<Chat> chats =
            await context
            .Chats
            .Where(
                x => x.UserId == userId && x.ToUserId == toUserId
                || x.ToUserId == userId && x.UserId == toUserId
                )
            .OrderBy(x => x.Date)
            .ToListAsync(cancellationToken);

        return Ok(chats);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(SendMessageDto request, CancellationToken cancellationToken)
    {
        Chat chat = new()
        {
            UserId = request.UserId,
            ToUserId = request.ToUserId,
            Message = request.Message,
            Date = DateTime.Now
        };

        await context.AddAsync(chat, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}
