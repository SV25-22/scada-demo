using CoreWCF;

namespace Shared.Contracts;

[ServiceContract(Name = "ISensorService", Namespace = "http://tempuri.org/")]
public interface ISensorService
{
    [OperationContract] void Start();
    [OperationContract] void Stop();
    [OperationContract] double GetLatest();
    [OperationContract] SensorSnapshot GetSnapshot(TimeSpan lookback);

    // NEW: lets the coordinator append a reconciled value to the sensor DB
    [OperationContract] void AppendReconciled(double value);
}
