using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Shared.Contracts;
using Coordinator.Host.Clients;

namespace Coordinator.Host;

public class CoordinatorService : ICoordinatorService
{
    private volatile bool _inProgress;
    private readonly SemaphoreSlim _mutex = new(1, 1);

    public bool IsReconInProgress() => _inProgress;

    public async Task<ReconResult> ReconcileAsync()
    {
        await _mutex.WaitAsync();
        _inProgress = true;
        try
        {
            var s1 = Sensor(Ports.S1);
            var s2 = Sensor(Ports.S2);
            var s3 = Sensor(Ports.S3);

            var x1 = s1.GetLatest();
            var x2 = s2.GetLatest();
            var x3 = s3.GetLatest();
            var avg = (x1 + x2 + x3) / 3.0;

            s1.AppendReconciled(avg);
            s2.AppendReconciled(avg);
            s3.AppendReconciled(avg);

            Close(s1); Close(s2); Close(s3);
            return new ReconResult(true, DateTimeOffset.UtcNow, avg, "Reconciled latest to average of three sensors.");
        }
        catch (Exception ex)
        {
            return new ReconResult(false, DateTimeOffset.UtcNow, double.NaN, $"Reconcile failed: {ex.Message}");
        }
        finally
        {
            _inProgress = false;
            _mutex.Release();
        }
    }

    private ISensorServiceClient Sensor(int port)
    {
        var binding = new BasicHttpBinding();
        var addr = new EndpointAddress($"http://localhost:{port}/sensor");
        var factory = new ChannelFactory<ISensorServiceClient>(binding, addr);
        return factory.CreateChannel();
    }

    private static void Close(object ch)
    {
        try { if (ch is IClientChannel cc) cc.Close(); }
        catch { if (ch is IClientChannel cc2) cc2.Abort(); }
    }
}
