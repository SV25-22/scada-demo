using CoreWCF;

namespace Shared.Contracts;

[ServiceContract]
public interface ISensorService
{
    [OperationContract]
    void Start();

    [OperationContract]
    void Stop();

    [OperationContract]
    double GetLatest();

    [OperationContract]
    SensorSnapshot GetSnapshot(TimeSpan lookback);
}
