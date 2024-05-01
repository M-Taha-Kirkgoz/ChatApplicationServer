using ChatApplicationServer.Context;
using ChatApplicationServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplicationServer.Hubs;

public sealed class ChatHub(
    ApplicationDbContext context) : Hub
{
    public static Dictionary<string, Guid> Users = new();

    public async Task Connect(Guid userId)
    {
        Users.Add(Context.ConnectionId, userId);
        User? user = await context.Users.FindAsync(userId);

        if (user is not null)
        {
            user.Status = "online";
            await context.SaveChangesAsync();

            await Clients.All.SendAsync("Users", user);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Guid userId;
        Users.TryGetValue(Context.ConnectionId, out userId);

        Users.Remove(Context.ConnectionId);

        User? user = await context.Users.FindAsync(userId);

        if (user is not null)
        {
            user.Status = "offline";
            await context.SaveChangesAsync();

            await Clients.All.SendAsync("Users", user); // Tüm kullanıcılara; çıkış yapan kullanıcının, çıkış yaptığına dair bilginin gitmesi için.
        }
    }
}
