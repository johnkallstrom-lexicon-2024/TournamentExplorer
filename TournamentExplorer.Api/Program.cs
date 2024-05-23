using System.Reflection;
using TournamentExplorer.Api.Extensions;
using TournamentExplorer.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config =>
{
    config.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

// Used to add extra information on the Problem Details object 

//builder.Services.AddProblemDetails(options =>
//{
//    options.CustomizeProblemDetails = ctx =>
//    {
//        ctx.ProblemDetails.Extensions.Add("extra", "hello world!");
//    };
//});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //await app.SeedDatabase();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
