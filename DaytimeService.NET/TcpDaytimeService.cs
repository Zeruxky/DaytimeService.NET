// <copyright file="TcpDaytimeService.cs" author="Philip 'Zeruxky' Wille">
// Copyright (c) Philip 'Zeruxky' Wille. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DaytimeService.NET
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides functionality to run a daytime service using TCP.
    /// </summary>
    /// <remarks>
    /// Fully accomplished to RFC 867.
    /// </remarks>
    /// <seealso cref="System.IDisposable" />
    public class TcpDaytimeService : DaytimeService, IDisposable
    {
        private readonly TcpListener listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpDaytimeService"/> class.
        /// </summary>
        /// <remarks>
        /// The default port of a <see cref="TcpDaytimeService"/> is <value>13</value> according to RFC 867.
        /// </remarks>
        public TcpDaytimeService()
            : this(13)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpDaytimeService"/> class.
        /// </summary>
        /// <param name="port">The port on which the <see cref="TcpDaytimeService"/> should listening.</param>
        public TcpDaytimeService(int port)
            : this(new IPEndPoint(IPAddress.Loopback, port))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpDaytimeService"/> class.
        /// </summary>
        /// <param name="endPoint">The <see cref="IPEndPoint"/> with all data for setting up the <see cref="TcpDaytimeService"/>.</param>
        public TcpDaytimeService(IPEndPoint endPoint)
        {
            this.listener = new TcpListener(endPoint);
        }

        /// <inheritdoc cref="DaytimeService"/>
        public override async Task RunAsync()
        {
            this.listener.Start();
            await Console.Out.WriteLineAsync($"Running daytime service on port {this.listener.LocalEndpoint} with connection type {this.listener.Server.ProtocolType}.").ConfigureAwait(false);
            try
            {
                while (true)
                {
                    await Console.Out.WriteLineAsync("Waiting for request...").ConfigureAwait(false);

                    using (var client = await this.listener.AcceptTcpClientAsync().ConfigureAwait(false))
                    {
                        await Console.Out.WriteLineAsync($"Received request from {client.Client.RemoteEndPoint}.").ConfigureAwait(false);
                        var now = DateTimeOffset.Now;
                        var content = ToBytes(now);

                        using (var stream = client.GetStream())
                        {
                            await Console.Out.WriteLineAsync($"Sending response to {client.Client.RemoteEndPoint}.").ConfigureAwait(false);
                            await stream.WriteAsync(content, 0, content.Length).ConfigureAwait(false);
                        }
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                this.listener.Server.Close();
                this.listener.Stop();
            }
        }

        /// <inheritdoc cref="IDisposable"/>
        public void Dispose()
        {
            this.listener?.Server?.Close();
            this.listener?.Stop();
        }
    }
}