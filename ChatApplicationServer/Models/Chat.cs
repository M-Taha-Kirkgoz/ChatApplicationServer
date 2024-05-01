namespace ChatApplicationServer.Models;

public sealed class Chat
{
    public Chat()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id {  get; set; }
    public Guid UserId { get; set; } // Kim sohbet ediyor ?
    public Guid ToUserId { get; set; } // Kim ile sohbet ediyor ?
    public string Message { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
