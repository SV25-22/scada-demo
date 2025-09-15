using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Shared.Contracts;
using Coordinator.Host;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5020");
builder.Services.AddServiceModelServices();

builder.Services.AddSingleton<CoordinatorService>();
builder.Services.AddHostedService<ReconcilerWorker>();

var app = builder.Build();
app.UseServiceModel(s =>
{
    s.AddService<CoordinatorService>();
    s.ConfigureServiceHostBase<CoordinatorService>(host =>
    {
        var dbg = host.Description.Behaviors.Find<ServiceDebugBehavior>();
        if (dbg == null)
            host.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
        else
            dbg.IncludeExceptionDetailInFaults = true;
    });
    s.AddServiceEndpoint<CoordinatorService, ICoordinatorService>(new BasicHttpBinding(), "/coord");
});
Console.WriteLine("Coordinator listening at http://localhost:5020/coord (BasicHttpBinding)");
app.Run();
