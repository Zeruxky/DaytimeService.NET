using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using CommandLine;

namespace DaytimeService.NET.CLI
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var parser = new Parser(with =>
            {
                with.AutoHelp = true;
                with.AutoVersion = true;
                with.EnableDashDash = true;
                with.HelpWriter = Console.Out;
                with.CaseInsensitiveEnumValues = true;
            });

            await parser.ParseArguments<ServiceArguments>(args).WithParsedAsync(RunAsync).ConfigureAwait(false);
        }

        private static async Task RunAsync(ServiceArguments arguments)
        {
            if (arguments.Type == ProtocolType.Tcp)
            {
                using (var service = new TcpDaytimeService(arguments.Port))
                {
                    await service.RunAsync().ConfigureAwait(false);
                }
            }
            
            if (arguments.Type == ProtocolType.Udp)
            {
                using (var service = new UdpDaytimeService(arguments.Port))
                {
                    await service.RunAsync().ConfigureAwait(false);
                }
            }

            throw new ArgumentException($"Unknown connection type {arguments.Type}.");
        }
    }
}
