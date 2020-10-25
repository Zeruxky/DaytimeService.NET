using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DaytimeService.NET.TestClient
{
    public static class TestClient
    {
        private static readonly Random Random = new Random();
        
        public static async Task RunAsync(ClientArguments arguments)
        {
            var srcPort = GetSrcPort(arguments);
            var srcEndpoint = new IPEndPoint(IPAddress.Loopback, srcPort);
            var descEndpoint = new IPEndPoint(IPAddress.Loopback, arguments.DestinationPort);

            if (arguments.Type == ProtocolType.Tcp)
            {
                await CommunicateOverTcp(srcEndpoint, descEndpoint).ConfigureAwait(false);
            }

            if (arguments.Type == ProtocolType.Udp)
            {
                await CommunicateOverUdp(srcEndpoint, descEndpoint).ConfigureAwait(false);
            }
        }

        private static async Task CommunicateOverUdp(IPEndPoint srcEndpoint, IPEndPoint descEndpoint)
        {
            while (true)
            {
                try
                {
                    using (var client = new UdpClient(srcEndpoint))
                    {
                        var content = Encoding.ASCII.GetBytes(string.Empty);
                        await client.SendAsync(content, 0, descEndpoint).ConfigureAwait(false);

                        var result = await client.ReceiveAsync().ConfigureAwait(false);
                        var response = Encoding.ASCII.GetString(result.Buffer);
                        var datetime = DateTimeOffset.Parse(response);
                        await Console.Out.WriteLineAsync($"Received from {descEndpoint}: {datetime:R}")
                            .ConfigureAwait(false);

                        await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static async Task CommunicateOverTcp(IPEndPoint srcEndpoint, IPEndPoint descEndpoint)
        {
            while (true)
            {
                try
                {
                    using (var client = new TcpClient(srcEndpoint))
                    {
                        await client.ConnectAsync(descEndpoint.Address, descEndpoint.Port).ConfigureAwait(false);
                        using (var stream = client.GetStream())
                        {
                            var data = new byte[256];
                            var buffer = new Memory<byte>(data);
                            await stream.ReadAsync(buffer).ConfigureAwait(false);
                            var response = Encoding.ASCII.GetString(data);
                            var datetime = DateTimeOffset.Parse(response);
                            await Console.Out.WriteLineAsync($"Received from {client.Client.RemoteEndPoint}: {datetime:R}")
                                .ConfigureAwait(false);
                        }

                        await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static int GetSrcPort(ClientArguments arguments)
        {
            var srcPort = arguments.SourcePort;
            if (srcPort < IPEndPoint.MinPort)
            {
                srcPort = Random.Next(IPEndPoint.MinPort, IPEndPoint.MaxPort);
            }

            return srcPort;
        }
    }
}