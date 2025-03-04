var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Qui inseriremo il codice per la connessione al database (in questo caso SQLite)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Aggiungiamo il servizio per la gestione degli utenti
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlite(connectionString));

#region Seeding
if (Seeding.IsEmpty()) {
    // aggiungiamo i prodotti
    Seeding.AddProduct("Pasta", 1.5m);
    Seeding.AddProduct("Pane", 2.5m);
    Seeding.AddProduct("Latte", 1.0m);
    Seeding.AddProduct("Uova", 3.0m);
    Seeding.AddProduct("Pomodori", 2.0m);
}

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
