namespace WebAppMVC_EF_SQLite.Data;

// facciamo una piccola utility di seeding del database in modo da inizializzarlo.
// Utilizzaremo i metodi di Add e SaveChanges per inserire i dati nel database.

public static class Seeding {


    // creiamo un metodo statico per aggiungere un prodotto
    public static void AddProduct(string name, decimal price) {
        // dentro uno using andiamo ad aggiungere il prodotto e salvare i cambiamenti
        using (var context = new AppDBContext()) {
            // la sintassi del comando sarà Context.NomeTabella.Add(OggettoDaInserire)
            context.Products.Add(
                    new Product { 
                        Name = name, 
                        Price = price 
                        }
                    );
            
            // salviamo i cambiamenti
            context.SaveChanges();
        }
    }

    // creiamo un metodo per capire se la taballe è vuota
    public static bool IsEmpty() {

        using var context = new AppDBContext();
        // se il conteggio dei prodotti è 0, la tabella è vuota
        Console.WriteLine($"Products count: {context.Products.Count()}");
        return context.Products.Count() == 0;
    }
}