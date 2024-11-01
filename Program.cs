// Program.cs
using Microsoft.EntityFrameworkCore;
using QBankApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o DbContext com a string de conexão
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Demais configurações...
app.Run();