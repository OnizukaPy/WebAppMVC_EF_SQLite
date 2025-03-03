namespace WebAppMVC_EF_SQLite.Models;

public class Order {

    // la classe avrà un Id, una data e un costo
    // che verranno mappati sul database
    // Id sarà la chiave primaria
    [Key]
    public int Id { get; set; }
    // La data di creazione sarà un datetime e verrà mappata come "datetime"
    [Column(TypeName = "datetime")]
    public DateTime OrderPlaced { get; set; }
    // La data di conclusione che può essere nulla
    [Column(TypeName = "datetime")]
    public DateTime? OrderFulfilled { get; set; }

    // L'Id del costumer che ha effettuato l'ordine
    public int CostumerId { get; set; }
    // Il costumer che ha effettuato l'ordine
    public Costumer Costumer { get; set; } = null!;
    // una collezione di dettagli dell'ordine che andremo a definire dopo
    public ICollection<OrderDetail> OrderDetails { get; set; } = null!;
}