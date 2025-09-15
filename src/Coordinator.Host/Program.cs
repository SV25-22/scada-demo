using CoreWCF;
using CoreWCF.Configuration;
using Shared.Contracts;
using Coordinator.Host;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5020");
builder.Services.AddServiceModelServices();

builder.Services.AddSingleton<CoordinatorService>();

var app = builder.Build();
app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<CoordinatorService>();
    serviceBuilder.AddServiceEndpoint<CoordinatorService, ICoordinatorService>(new BasicHttpBinding(), "/coord");
});

app.Run();
