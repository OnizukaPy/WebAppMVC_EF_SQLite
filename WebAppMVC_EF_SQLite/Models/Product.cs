namespace WebAppMVC_EF_SQLite.Models;

public class Product {
    // Il prodotto avrà un Id, un Nome e un Prezzo
    // che verranno mappati sul database
    // Id sarà la chiave primaria
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    // Il prezzo sarà un decimale con 6 cifre di cui 2 decimali
    // e verrà mappato come "decimal(6, 2)" e sarà una colonna del database con un tipo specifico
    [Column(TypeName = "decimal(6, 2)")]
    public decimal Price { get; set; }
}
