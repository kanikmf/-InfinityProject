using System.Text.Json.Serialization;
using WS.Business.Implementations;
using WS.Business.Interfaces;
using WS.DataAccess.Implementations.EF.Repositories;
using WS.DataAccess.Interfaces;
using WS.WebAPI.Extensions;
using WS.Business.Extensions;
using WS.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAPIServices();
builder.Services.AddBusinessServices();
// Add services to the container.


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();
app.UseCustomException();

app.Run();
