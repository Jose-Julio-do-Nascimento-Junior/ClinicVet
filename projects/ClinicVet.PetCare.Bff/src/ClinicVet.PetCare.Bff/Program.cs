using ClinicVet.Core.Api.Http;
using ClinicVet.Core.Infra.Services.Http;
using ClinicVet.PetCare.Bff;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddAppContext<Program>(configuration);
builder.Services.AddApplicationBootstrapper(configuration);
builder.Services.AddServicesContext(configuration);
builder.Services.AddDistributedCache(configuration);

var app = builder.Build();
app.UseApplicationContext();
app.UseCors();
app.Run();