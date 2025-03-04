namespace WebAppMVC_EF_SQLite.Data;

// facciamo una piccola utility di seeding del database in modo da inizializzarlo.
// Utilizzaremo i metodi di Add e SaveChanges per inserire i dati nel database.

public static class Seeding {

    // Dichiariamo un manipolatore
    static ManipulateDbContext? _manipulateDbContext = new();

    // creiamo un metodo statico per aggiungere un prodotto
    public static void AddProduct(string name, decimal price) {
        
        // la sintassi del comando sarà Context.NomeTabella.Add(OggettoDaInserire)
        _manipulateDbContext?._context?.Products.Add(
                new Product { 
                    Name = name, 
                    Price = price 
                    }
                );
        
        // salviamo i cambiamenti
        _manipulateDbContext?._context?.SaveChanges();
        
    }

    // creiamo un metodo per capire se la taballe è vuota
    public static bool IsEmpty() {

        // se il conteggio dei prodotti è 0, la tabella è vuota
        Console.WriteLine($"Products count: {_manipulateDbContext?._context?.Products.Count()}");
        return _manipulateDbContext?._context?.Products.Count() == 0;
    }
}