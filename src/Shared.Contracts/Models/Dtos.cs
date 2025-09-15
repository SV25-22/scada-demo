using System.Runtime.Serialization;

namespace Shared.Contracts;

[DataContract]
public class SensorSnapshot
{
    [DataMember(Order = 1)] public string SensorId { get; set; } = "";
    [DataMember(Order = 2)] public DateTimeOffset From { get; set; }
    [DataMember(Order = 3)] public DateTimeOffset To { get; set; }
    // Prefer array/List over interface types for DataContractSerializer
    [DataMember(Order = 4)] public double[] Values { get; set; } = Array.Empty<double>();

    public SensorSnapshot() { }
}

[DataContract]
public class ReconResult
{
    [DataMember(Order = 1)] public bool Success { get; set; }
    [DataMember(Order = 2)] public DateTimeOffset At { get; set; }
    [DataMember(Order = 3)] public double AveragedValue { get; set; }
    [DataMember(Order = 4)] public string Message { get; set; } = "";

    public ReconResult() { }
}
