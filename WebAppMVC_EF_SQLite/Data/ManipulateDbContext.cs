namespace WebAppMVC_EF_SQLite.Data;

// questa classe la useremo per manipolare il database sia in visualizzazione che in inserimento
public class ManipulateDbContext {

        // Inizializziamo la variabile del Contesto del database
    public readonly AppDBContext? _context = null;
    // inizializiamo la option per il contesto del database
    private readonly DbContextOptions<AppDBContext>? _options = null;

    // il costruttore della classe accetta un parametro di tipo AppDBContext
    public ManipulateDbContext() {
        // inizializziamo il contesto passato come parametro
        _options = new DbContextOptionsBuilder<AppDBContext>()
            .UseSqlite("Data Source=WebAppMVC_EF_SQLite.db")
            .Options;
        _context = new AppDBContext(_options);
        
    }

    // metodo pubblico per visualizzare tutti gli elementi della tabella Products
    public List<Product> ShowAllProducts() {
        // se il contesto è nullo, usciamo dal metodo
        if (_context == null) {
            return new List<Product>();
        }

        // per visualizzare tutti gli elementi della tabella Products
        // possiamo usare il metodo ToList() che restituisce una lista di tutti gli elementi
        // della tabella Products
        var products = _context.Products.ToList();
        return products;
    }
}