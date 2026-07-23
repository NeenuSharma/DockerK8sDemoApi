using DockerK8sDemoApi.Context;
using DockerK8sDemoApi.Repository;
using DockerK8sDemoApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataBaseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ProdcutServiceRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// --- NEW: wait for SQL Server + apply migrations, with retries ---
// SQL Server pods take 15-30s to become ready; without this the app
// crashes on startup before the database is reachable.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataBaseDbContext>();
    int attempts = 0;
    while (true)
    {
        try
        {
            db.Database.EnsureCreated();   // applies any pending EF Core migrations
            // If you are NOT using Migrations, replace the line above with:
            // db.Database.EnsureCreated();
            Console.WriteLine("Database ready.");
            break;
        }
        catch (Exception ex)
        {
            attempts++;
            if (attempts >= 15)
                throw;
            Console.WriteLine($"Waiting for database... attempt {attempts} ({ex.Message})");
            Thread.Sleep(3000);
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --- CHANGED: HTTPS redirection removed for container use ---
// Containers don't have a cert configured; we serve plain HTTP internally.
// (Real deployments handle HTTPS at a load balancer/ingress in front of the pod.)
//app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();