# Entity Framework

[VideoGuide](https://www.youtube.com/playlist?list=PLdo4fOcmZ0oX7uTkjYwvCJDG2qhcSzwZ6)

## [Ottenere gli strumenti dell'interfaccia della riga di comando di .NET Core](https://learn.microsoft.com/it-it/ef/core/get-started/overview/install#get-the-net-core-cli-tools)

Per installare Entity Framework Core si deve eseguire il comando:

```bash
dotnet tool install --global dotnet-ef
```

Per aggiornare gli strumenti, usare il `dotnet tool update` comando ed installare la libreria di Entity Framework Core con il comando:

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.12
```

## Creare un progetto di Entity Framework Core con SQLite

La prima cosa da fare è scegliere il [provider](https://learn.microsoft.com/it-it/ef/core/providers/?tabs=dotnet-core-cli) di entityFramework che si vuole installare, il quale dipende dal tipo di database che si vuole usare. In questo caso si vuole usare un database SQLite, quindi si deve installare il provider per SQLite.

Per installare il provider per SQLite si deve eseguire il comando:

```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.12
```

Poi installiamo le librerie per gestire eventuali altre interazioni con il database

```bash
dotnet add package System.Data.SQLite
```

Seguirò le istruzioni nella [panoramica di Microsoft per creare un database SQLite con Entity Framework Core](https://learn.microsoft.com/it-it/ef/core/providers/sqlite/?tabs=dotnet-core-cli).

### Limitazioni di SQLite

- `di modellazione`:

Il provider SQLite presenta una serie di limitazioni delle migrazioni. La maggior parte di queste limitazioni è causata da limitazioni nel motore di database SQLite sottostante e non sono specifiche di ENTITY.

La libreria relazionale comune (condivisa dai provider di database relazionali di EF Core) definisce le API per la modellazione dei concetti comuni alla maggior parte dei motori di database relazionali. Un paio di questi concetti non sono supportati dal provider SQLite.

    - Schemi
    - Sequenze
    - Token di concorrenza generati dal database

- `di query`:

SQLite non supporta in modo nativo i tipi di dati seguenti. EF Core può leggere e scrivere valori di questi tipi ed è supportata anche l'esecuzione di query per verificarne l'uguaglianza (`where e.Property == value`). Altre operazioni, tuttavia, come il confronto e l'ordinamento richiederanno la valutazione sul client.

    - DateTimeOffset
    - decimal
    - TimeSpan
    - ulong

*Anziché `DateTimeOffset`, è consigliabile usare valori di `DateTime`. Quando si gestiscono più fusi orari, è consigliabile convertire i valori in UTC prima di salvare e quindi riconvertire il fuso orario appropriato.*

*Il `decimal` tipo fornisce un livello elevato di precisione. Se non è necessario tale livello di precisione, tuttavia, è consigliabile usare `double`. È possibile usare un convertitore di valori per continuare a usare decimal nelle classi*

```csharp
modelBuilder.Entity<MyEntity>()
.Property(e => e.DecimalProperty)
.HasConversion<double>();
```

- `di migrazione`: Rimando alla [tabella](https://learn.microsoft.com/it-it/ef/core/providers/sqlite/limitations#migrations-limitations) per capire quali sono le migrazioni non consentite. È possibile risolvere alcune di queste limitazioni scrivendo manualmente codice nelle migrazioni per eseguire una ricompilazione. Le ricompilazione delle tabelle comportano la creazione di una nuova tabella, la copia dei dati nella nuova tabella, l'eliminazione della tabella precedente, la ridenominazione della nuova tabella. Per eseguire alcuni di questi passaggi, è necessario usare il Sql metodo. E' consigliabile usare dotnet ef database update per applicare le migrazioni. È possibile specificare il file di database quando si esegue il comando . Se si specifica un file di database, verrà creato se non esiste. Se si specifica un file di database esistente, verrà utilizzato come database di destinazione per le migrazioni.

```bash
dotnet ef database update --connection "Data Source=My.db"
```

### Mapping delle funzioni con le operazioni SQL

La guida di tutte le funzioni mappate si trova [qui](https://learn.microsoft.com/it-it/ef/core/providers/sqlite/functions).

## Creazione di una Web App MVC

Per creare una web app MVC si deve eseguire il comando:

```bash
dotnet new mvc -n NomeProgetto
```

### Creazione di un modello

Dopo di che la prima cosa da creare sono i modelli. EF lavora con i modelli e le migrazioni.

Ad esempio creiamo un piccolo sito con un prodotti, ordini e clienti. Come prima cosa occorre creare una classe per la creazione del database contenente i modelli e il DBContext.

Es: `Models/Product.cs`:

```csharp
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
```

Poi si andrà a creare la classe di `DBContext` che mappi i modelli con le tabelle del database. Questa la metteremo in una cartella Data. Questo file conterrà il contesto e i modelli che verranno mappati nel database.

Es: `Data/AppDBContext.cs`:

```csharp
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

    // il metodo OnConfiguring che verrà chiamato quando il contesto viene configurato
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        // verrà configurato per utilizzare il provider SQLite
        // e verrà passato il nome del file del database
        optionsBuilder.UseSqlite("Data Source=WebAppMVC_EF_SQLite.db");
    }
}
```

Si può utilizzare un costruttore dentro la classe per passare delle opzioni al contesto del database. Aggiungendo il costruttore con un argomento poi ovunque viene usato dovrà essere ripetuta la stessa configurazione.

```csharp
// il costruttore della classe
public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
```

Nella classe poi di manipolazione del Database inseriremo un costruttore che farà questo

```csharp
public ManipulateDbContext() {
    // inizializziamo il contesto passato come parametro
    _options = new DbContextOptionsBuilder<AppDBContext>()
        .UseSqlite("Data Source=WebAppMVC_EF_SQLite.db")
        .Options;
    _context = new AppDBContext(_options);
    
}
```

Per poi nel `Program.cs` andare a configurare il contesto del database.

```csharp
// Qui inseriremo il codice per la connessione al database (in questo caso SQLite)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=WebAppMVC_EF_SQLite.db";
// Aggiungiamo il servizio per la gestione degli utenti
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlite(connectionString));
```

Se si fa questa modifica non occorre più il metodo `OnConfiguring` nel contesto del database.

Una volta creato il modello occorre lanciare la creazione del database mediante i comandi di migrazione di dotnet:

```bash
# La sintassi è la seguente:
# dotnet ef migrations add NomeMigrazione
dotnet ef migrations add InitialCreate
# per applicare la migrazione al database
dotnet ef database update
```

### Aggiornamento della struttura del database

Per aggiornare la struttura del database, il passaggio da fare è il seguente: 

1. Modificare il modello aggiungendo il campo che si vuole aggiungere o modificare, oppure rimuovere
2. Creare una nuova migrazione con il comando `dotnet ef migrations add NomeMigrazione`
3. Applicare la migrazione al database con il comando `dotnet ef database update`

### `DA RIVEDERE` - Utilizzo delle Secret di ASP.NET Core per gestire le stringe di connessione

Per poter utilizzare le secret occorre installare il pacchetto `Microsoft.Extensions.Configuration.UserSecrets` e `Microsoft.Extensions.Configuration.UserSecrets.Tools`.

```bash
# Installiamo i pachetti
dotnet add package Microsoft.Extensions.Configuration.UserSecrets --version 8.0.1
dotnet add package Microsoft.Extensions.SecretManager.Tools --version 2.0.2
```
# inizializzare le secret
dotnet user-secrets init
# aggiungere una secret
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Data Source=WebAppMVC_EF_SQLite.db"
```

Creare poi un json con le seguenti informazioni. andrà poi salvato in una cartella nascosta `.secrets` nella cartella del progetto.

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Data Source=WebAppMVC_EF_SQLite.db"
    }
}
```

File di `GlobalUsing.cs`:

```csharp
global using Microsoft.EntityFrameworkCore;
global using System;
global using System.IO;
global using System.Collections.Generic;
global using System.Data.SQLite;

// Data annotation per i modelli
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;

// using dei modelli
global using WebAppMVC_EF_SQLite.Models;
// using per usare DBContext
global using WebAppMVC_EF_SQLite.Data;
```