using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Coordinator.Host;

public class ReconcilerWorker : BackgroundService
{
    private readonly ILogger<ReconcilerWorker> _log;
    private readonly CoordinatorService _svc;

    public ReconcilerWorker(ILogger<ReconcilerWorker> log, CoordinatorService svc)
    {
        _log = log;
        _svc = svc;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _log.LogInformation("ReconcilerWorker started");
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            if (stoppingToken.IsCancellationRequested) break;

            _log.LogInformation("Triggering scheduled reconciliation...");
            var res = await _svc.ReconcileAsync();
            _log.LogInformation("Reconcile result: Success={Success}, Avg={Avg}, Msg={Msg}",
                res.Success, res.AveragedValue, res.Message);
        }
    }
}
