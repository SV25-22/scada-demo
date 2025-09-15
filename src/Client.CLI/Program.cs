using System;
using System.Linq;
using System.ServiceModel;
using Client.CLI.Contracts;
using Shared.Contracts;

class Program
{
    static void Main()
    {
        var tol = 0.5;

        var coord = Coord();
        while (coord.IsReconInProgress())
        {
            Console.WriteLine("Reconciliation in progress... waiting...");
            System.Threading.Thread.Sleep(1000);
        }

        var s1 = Sensor(Ports.S1);
        var s2 = Sensor(Ports.S2);
        var s3 = Sensor(Ports.S3);

        double x1 = s1.GetLatest();
        double x2 = s2.GetLatest();
        double x3 = s3.GetLatest();

        double mean = (x1 + x2 + x3) / 3.0;
        int inliers = new[] { x1, x2, x3 }.Count(v => Math.Abs(v - mean) <= tol);

        if (inliers >= 2)
        {
            var accepted = new[] { x1, x2, x3 }.Where(v => Math.Abs(v - mean) <= tol).Average();
            Console.WriteLine($"Accepted quorum value: {accepted:F2} (mean={mean:F2}, tol=±{tol})");
        }
        else
        {
            Console.WriteLine("No quorum (±5). Triggering reconciliation...");
            var res = coord.ReconcileAsync().Result;
            Console.WriteLine($"Reconcile result: Success={res.Success}, Avg={res.AveragedValue:F2}, Msg={res.Message}");

            while (coord.IsReconInProgress())
            {
                Console.WriteLine("Reconciliation in progress... waiting...");
                System.Threading.Thread.Sleep(1000);
            }

            x1 = s1.GetLatest();
            x2 = s2.GetLatest();
            x3 = s3.GetLatest();
            mean = (x1 + x2 + x3) / 3.0;
            Console.WriteLine($"Post-reconcile latests: [{x1:F2}, {x2:F2}, {x3:F2}] mean={mean:F2}");
        }

        Close(s1); Close(s2); Close(s3);
        Close(coord);
    }

    static ISensorServiceClient Sensor(int port)
    {
        var binding = new BasicHttpBinding();
        var address = new EndpointAddress($"http://localhost:{port}/sensor");
        var factory = new ChannelFactory<ISensorServiceClient>(binding, address);
        return factory.CreateChannel();
    }

    static ICoordinatorServiceClient Coord()
    {
        var binding = new BasicHttpBinding();
        var address = new EndpointAddress($"http://localhost:{Ports.Coordinator}/coord");
        var factory = new ChannelFactory<ICoordinatorServiceClient>(binding, address);
        return factory.CreateChannel();
    }

    static void Close(object ch)
    {
        try { if (ch is IClientChannel cc) cc.Close(); }
        catch { if (ch is IClientChannel cc2) cc2.Abort(); }
    }
}
