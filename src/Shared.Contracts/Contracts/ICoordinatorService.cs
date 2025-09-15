using CoreWCF;

namespace Shared.Contracts;

[ServiceContract]
public interface ICoordinatorService
{
    [OperationContract]
    Task<ReconResult> ReconcileAsync();

    [OperationContract]
    bool IsReconInProgress();
}
