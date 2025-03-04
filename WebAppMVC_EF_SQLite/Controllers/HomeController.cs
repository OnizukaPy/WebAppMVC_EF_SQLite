namespace WebAppMVC_EF_SQLite.Controllers;

public class HomeController : Controller {

    // Per istanziare il logger occorre dichiarare una variabile di tipo ILogger<T>
    // dove T è il tipo della classe che sta utilizzando il logger
    private readonly ILogger<HomeController>? _logger;
    public string MessageForView { get; set; } = "Hello from HomeController!";

    // Inizializziamo la variabile del Manipolatore del database
    private ManipulateDbContext? _manipulateDbContext = null;

    // Il costruttore della classe HomeController accetta un parametro di tipo ILogger<HomeController>
    // Questo parametro è un'istanza di un logger che verrà utilizzato per scrivere i log
    public HomeController (ILogger<HomeController> logger) {
        // Inizializiamo il logger passando come parametro il logger passato al costruttore
        _logger = InitLogger(logger);

        // Inizializziamo il manipolatore del database
        InitManipulateDbContext();

    }

    #region Init
    // Questo metodo inizializza il logger
    ILogger<HomeController>? InitLogger(ILogger<HomeController> logger) {
        
        if (_logger != null) {
            // printiamo a video un messaggio di log
            MessageForView = "Logger inizializzato con successo!";
            _logger.LogInformation(MessageForView);
            return _logger;
        } else {
            // Se si verifica un'eccezione la catturiamo e scriviamo un messaggio di errore
            MessageForView = "Errore durante l'inizializzazione del logger!";
            _logger?.LogError(MessageForView);
            return null;
        }
    }

    // Questo metodo inizializza il manipolatore del database
    void InitManipulateDbContext() {

        if (_manipulateDbContext == null) {
            _manipulateDbContext = new ManipulateDbContext();
            MessageForView = "Manipolatore del database inizializzato con successo!";
            _logger?.LogInformation(MessageForView);
        } 
    }
    #endregion

    // Questo metodo restituisce la vista Index
    // La vista Index è la vista principale della pagina
    public IActionResult Index() {
        // il metoto View restituisce la vista con il nome del metodo chiamante
        // in questo caso restituisce la vista Index, presente nella cartella Views/Home
        ViewData["MessageToView"] = MessageForView;
        // alla vista passiamo un elenco dei dati contenuti nella tabella Products del database
        var products = _manipulateDbContext?.ShowAllProducts();
        // restituiamo la vista con i dati passati come parametro alla vista
        // nella vista questo poi sarà richiamabile con il nome dei variabile Model
        return View(products);
    }

    // Questo metodo restituisce la vista Privacy
    // La vista Privacy è la vista che mostra l'informativa sulla privacy
    public IActionResult Privacy() {
        ViewData["MessageToView"] = MessageForView;
        return View();
    }

    // Questo metodo restituisce la vista Error
    // La vista Error è la vista che mostra gli errori
    // Questo metodo è decorato con l'attributo ResponseCache che specifica che la risposta non deve essere memorizzata nella cache
    // e che non deve essere memorizzata in nessun luogo
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()  {
        return View(new ErrorViewModel { 
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
                    }
                );
    }
}
