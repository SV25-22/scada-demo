using System;
using System.ServiceModel;
using Shared.Contracts; // for SensorSnapshot DTO

namespace Client.CLI.Contracts
{
    [ServiceContract(Name = "ISensorService", Namespace = "http://tempuri.org/")]
    public interface ISensorServiceClient
    {
        [OperationContract] double GetLatest();
        [OperationContract] SensorSnapshot GetSnapshot(TimeSpan lookback);
        [OperationContract] void Start();
        [OperationContract] void Stop();
    }
}
