namespace ChatApplicationServer.Dtos;

public sealed record RegisterDto(
    string Name,
    IFormFile File
    ); 

// Sealed == Bir sınıfın miras alınamayacağı veya yeniden türetilemeyeceği anlamına gelir.
// record == Basit veri kümelerini temsil etmek için kullanılır.
