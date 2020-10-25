// <copyright file="TestClient.cs" author="Philip 'Zeruxky' Wille">
// Copyright (c) Philip 'Zeruxky' Wille. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DaytimeService.NET.TestClient
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides functionality to run the client to test the daytime service.
    /// </summary>
    public static class TestClient
    {
        private static readonly Random Random = new Random();

        /// <summary>
        /// Runs the test client with the given <paramref name="arguments"/>.
        /// </summary>
        /// <param name="arguments">The arguments to configure the <see cref="TestClient"/>.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous run operation.</returns>
        public static async Task RunAsync(ClientArguments arguments)
        {
            var srcPort = GetSrcPort(arguments);
            var srcEndpoint = new IPEndPoint(IPAddress.Loopback, srcPort);
            var descEndpoint = new IPEndPoint(IPAddress.Loopback, arguments.DestinationPort);

            if (arguments.Mode == ProtocolType.Tcp)
            {
                await CommunicateOverTcp(srcEndpoint, descEndpoint).ConfigureAwait(false);
            }

            if (arguments.Mode == ProtocolType.Udp)
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
                        await using (var stream = client.GetStream())
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