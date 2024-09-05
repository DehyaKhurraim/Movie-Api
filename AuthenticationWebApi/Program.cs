using AuthenticationWebApi;
using JwtAuthenticationManager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<JwtTokenHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.MapControllers();

app.Run();
