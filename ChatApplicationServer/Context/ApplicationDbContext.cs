using Microsoft.EntityFrameworkCore;

namespace ChatApplicationServer.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}
