using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TournamentExplorer.Api.ExceptionHandlers;
using TournamentExplorer.Api.Extensions;
using TournamentExplorer.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config =>
{
    config.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.SeedDatabase();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
