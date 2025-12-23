using ClinicVet.Core.Api.Http;
using ClinicVet.Core.Domain;
using ClinicVet.Core.Infra.Data.OracleSql;
using ClinicVet.PetCare.Api;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddAppContext<Program>(configuration);
builder.Services.AddDomainContext(configuration);
builder.Services.AddApplicationBootstrapper(configuration);
builder.Services.AddOracleContextWithDapper(configuration);
builder.Services.AddSettings(configuration);

var app = builder.Build();
app.UseApplicationContext();
app.UseCors();
app.Run();