using JwtAuthenticationManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieAPI;
using MovieAPI.Data;
using MovieAPI.Interface;
using MovieAPI.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers();
builder.Services.AddCustomJwtAuthentication();
builder.Services.AddEndpointsApiExplorer();

// Add DbContext with the PostgreSQL connection string
var connectionString = builder.Configuration.GetConnectionString("WebApiDatabase");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddSingleton<RabbitMQService>();

// Add Redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    string redisConnectionString = builder.Configuration.GetConnectionString("Redis");
    options.Configuration = redisConnectionString;
});

var app = builder.Build();

// Apply pending migrations
ApplyMigrations(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void ApplyMigrations(IApplicationBuilder app)
{
    using (var scope = app.ApplicationServices.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
}
