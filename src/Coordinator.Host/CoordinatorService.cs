using Shared.Contracts;

namespace Coordinator.Host;

public class CoordinatorService : ICoordinatorService
{
    public bool _inProgress;

    public bool IsReconInProgress() => _inProgress;

    public async Task<ReconResult> ReconcileAsync()
    {
        _inProgress = true;
        try
        {
            await Task.Delay(500); // placeholder

            return new ReconResult
            {
                Success = true,
                At = DateTimeOffset.UtcNow,
                AveragedValue = 0.0,
                Message = "Stub reconciliation completed."
            };
        }
        finally
        {
            _inProgress = false;
        }
    }

}
