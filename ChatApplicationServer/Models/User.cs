namespace ChatApplicationServer.Models;

public sealed class User
{

    public User()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; 
    // String olan değişkenlere null değer ataması yapılır. Çünkü stringler default da null gelir ve null warning hatası geri döner.
}
