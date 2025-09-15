using CoreWCF;
using CoreWCF.Configuration;
using Shared.Contracts;
using Sensor.Service;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5013");
builder.Services.AddServiceModelServices();

// Register the service instance with its sensorId
builder.Services.AddSingleton(new SensorService("S3"));

var app = builder.Build();
app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<SensorService>();
    serviceBuilder.AddServiceEndpoint<SensorService, ISensorService>(new BasicHttpBinding(), "/sensor");
});

app.Run();
