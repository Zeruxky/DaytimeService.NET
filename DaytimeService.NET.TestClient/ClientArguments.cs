using System.Net.Sockets;
using CommandLine;

namespace DaytimeService.NET.TestClient
{
    public class ClientArguments
    {
        [Option('s', "src-port", Default = -1, HelpText = "Set the source port of the client. If nothing is set, a free random port will be used.")]
        public int SourcePort { get; set; }

        [Option('d', "desc-port", Default = (int)13, HelpText = "Set the port of the to calling daytime service. Default will be used the port 13.")]
        public int DestinationPort { get; set; }

        [Option('c', "connection-type", Default = ProtocolType.Udp, HelpText = "Sets the connection type of the client. The type should match with that of the day time service.")]
        public ProtocolType Type { get; set; }
    }
}