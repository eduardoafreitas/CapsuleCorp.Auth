using Microsoft.EntityFrameworkCore;
using CapsuleCorp.Auth.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurating SQLite
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=CapsuleCorp.db"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Por padrão, ele procura em /swagger/v1/swagger.json
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
