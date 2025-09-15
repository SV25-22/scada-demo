using CoreWCF;
using CoreWCF.Configuration;
using Shared.Contracts;
using Sensor.Service;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5012");
builder.Services.AddServiceModelServices();

// Register the service instance with its sensorId
builder.Services.AddSingleton(new SensorService("S2"));

var app = builder.Build();
app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<SensorService>();
    serviceBuilder.AddServiceEndpoint<SensorService, ISensorService>(new BasicHttpBinding(), "/sensor");
});

app.Run();
