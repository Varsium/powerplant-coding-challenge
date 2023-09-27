using Engie.Application;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(config => 
{
#pragma warning disable CS8604 // Possible null reference argument.
    config = config.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(AssemblyPointer)?? throw new Exception("Assembly not found")));
#pragma warning restore CS8604 // Possible null reference argument.
});
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
