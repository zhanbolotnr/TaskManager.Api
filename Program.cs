using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Persistence;

var builder = WebApplication.CreateBuilder(args);

// подключаем контроллеры
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// маппим контроллеры
app.MapControllers();

app.Run();
