namespace WebAppMVC_EF_SQLite.Data;

// la classe sarà una classe pubblica
public class AppDBContext : DbContext {

    // Come proprietà avrà un DbSet di Product, Costumer, Order e OrderDetail
    // La classe DbSet è una classe generica che rappresenta un'entità del database
    // e verrà mappata sul database prendendo il nome della classe del modello.
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Costumer> Costumers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

    // il costruttore della classe
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    /* // il metodo OnConfiguring che verrà chiamato quando il contesto viene configurato
    // Per evitare rischi di possibile esposizione di dati sensibili si sposta questo nel Program.cs
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        // verrà configurato per utilizzare il provider SQLite
        // e verrà passato il nome del file del database
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlite("Data Source=WebAppMVC_EF_SQLite.db");
    } */

}