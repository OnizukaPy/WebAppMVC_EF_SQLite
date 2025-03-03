namespace WebAppMVC_EF_SQLite.Models;

public class OrderDetail {

    // la classe avrà un Id, una quantità e un prezzo
    // che verranno mappati sul database
    // Id sarà la chiave primaria
    [Key]
    public int Id { get; set; }
    // La quantità sarà un intero e verrà mappata come "int"
    [Column(TypeName = "int")]
    public int Quantity { get; set; }

    // L'Id del prodotto che è stato acquistato
    public int ProductId { get; set; }
    // L'Id dell'ordine a cui appartiene il dettaglio
    public int OrderId { get; set; }
    
    // Il prodotto che è stato acquistato
    public Product Product { get; set; } = null!;
    // L'ordine a cui appartiene il dettaglio
    public Order Order { get; set; } = null!;
}