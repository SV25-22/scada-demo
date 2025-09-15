using System;
using System.ServiceModel; 

namespace Shared.Contracts
{
    [ServiceContract(Name = "ISensorService", Namespace = "http://tempuri.org/")]
    public interface ISensorService
    {
        [OperationContract(Action = "http://tempuri.org/ISensorService/Start", ReplyAction = "*")]
        void Start();

        [OperationContract(Action = "http://tempuri.org/ISensorService/Stop", ReplyAction = "*")]
        void Stop();

        [OperationContract(Action = "http://tempuri.org/ISensorService/GetLatest", ReplyAction = "*")]
        double GetLatest();

        [OperationContract(Action = "http://tempuri.org/ISensorService/GetSnapshot", ReplyAction = "*")]
        SensorSnapshot GetSnapshot(TimeSpan lookback);

        // Let the coordinator append a reconciled value to the sensor DB
        [OperationContract(Action = "http://tempuri.org/ISensorService/AppendReconciled", ReplyAction = "*")]
        void AppendReconciled(double value);
    }
}
