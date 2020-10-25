using System.Net.Sockets;
using CommandLine;

namespace DaytimeService.NET.CLI
{
    public class ServiceArguments
    {
        [Option('c', "connection-type", Default = ProtocolType.Udp, Required = true, HelpText = "Sets the connection type of the daytime service.")]
        public ProtocolType Type { get; set; }

        [Option('s', "src-port", Default = 13, HelpText = "Sets the listening port of the daytime service.")]
        public int Port { get; set; }
    }
}