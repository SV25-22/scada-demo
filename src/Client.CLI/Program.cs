using System;
using System.ServiceModel;
using Client.CLI.Contracts;

class Program
{
    static void Main()
    {
        var binding = new BasicHttpBinding(); // defaults: No security
        var address = new EndpointAddress("http://localhost:5011/sensor");

        var factory = new ChannelFactory<ISensorServiceClient>(binding, address);
        var channel = factory.CreateChannel();

        var latest = channel.GetLatest();
        Console.WriteLine($"S1 latest: {latest}");

        ((IClientChannel)channel).Close();
        factory.Close();
    }
}
