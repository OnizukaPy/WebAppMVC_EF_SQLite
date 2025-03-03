namespace WebAppMVC_EF_SQLite.Models;

public class Costumer {
    // il costumer avrà un Id, un Nome e un Cognome
    // che verranno mappati sul database
    // Id sarà la chiave primaria
    [Key]
    public int Id { get; set; }
    // Il ! indica che non sarà mai null
    [Required]
    public string Name { get; set; } = null!;
    // Il ! indica che non sarà mai null
    [Required]
    public string Surname { get; set; } = null!;
    // aggiungiamo la mail che può essere nulla
    public string? Email { get; set; }

    // poi un indirizzo e un numero di telefono
    public string? Address { get; set; }
    public string? Phone { get; set; }

    // e una lista di ordini che definiremo in un'altra classe. 
    // Il ! indica che non sarà mai null
    public ICollection<Order> Orders { get; set; } = null!;
}