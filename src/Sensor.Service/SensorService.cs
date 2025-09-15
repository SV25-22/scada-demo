using Shared.Contracts;

namespace Sensor.Service;

public class SensorService : Shared.Contracts.ISensorService
{
    private readonly string _sensorId;
    private readonly Random _rng = new();
    private double _last = 20.0; // placeholder
    private bool _running;

    public SensorService(string sensorId)
    {
        _sensorId = sensorId;
    }

    public void Start()
    {
        _running = true;
        // Phase 1 will replace this with a real background loop + EF writes
    }

    public void Stop() => _running = false;

    public double GetLatest()
    {
        // Phase 1: read latest from DB. For now, return a jittered value.
        var delta = (_rng.NextDouble() - 0.5) * 0.5;
        _last = Math.Clamp(_last + delta, -30, 60);
        return _last;
    }

    public SensorSnapshot GetSnapshot(TimeSpan lookback)
    {
        var now = DateTimeOffset.UtcNow;
        var values = Enumerable.Range(0, 10).Select(_ => GetLatest()).ToArray();

        return new SensorSnapshot
        {
            SensorId = _sensorId,
            From = now - lookback,
            To = now,
            Values = values
        };
    }

}
