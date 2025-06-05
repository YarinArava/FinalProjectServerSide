using ClassLibrary1.Repository;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<EventService>(); // Controller doesn't approach to DataBase by itself
builder.Services.AddScoped<EventRepository>(); // Controller doesn't approach to DataBase by itself
builder.Services.AddMemoryCache(); // Synchronize the days, for the event weather

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
