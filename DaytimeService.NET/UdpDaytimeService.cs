// <copyright file="UdpDaytimeService.cs" author="Philip 'Zeruxky' Wille">
// Copyright (c) Philip 'Zeruxky' Wille. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DaytimeService.NET
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Provides functionality to run a daytime service using UDP.
    /// </summary>
    /// <remarks>
    /// Fully accomplished to RFC 867.
    /// </remarks>
    /// <seealso cref="System.IDisposable" />
    public class UdpDaytimeService : DaytimeService, IDisposable
    {
        private readonly UdpClient listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpDaytimeService"/> class.
        /// </summary>
        /// <remarks>
        /// The default port of a <see cref="UdpDaytimeService"/> is <value>13</value> according to RFC 867.
        /// </remarks>
        public UdpDaytimeService()
            : this(13)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpDaytimeService"/> class.
        /// </summary>
        /// <param name="port">The port on which the <see cref="UdpDaytimeService"/> should listening.</param>
        public UdpDaytimeService(int port)
            : this(new IPEndPoint(IPAddress.Loopback, port))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpDaytimeService"/> class.
        /// </summary>
        /// <param name="endPoint">The <see cref="IPEndPoint"/> with all data for setting up the <see cref="UdpDaytimeService"/>.</param>
        public UdpDaytimeService(IPEndPoint endPoint)
        {
            this.listener = new UdpClient(endPoint);
        }

        /// <inheritdoc />
        public override async Task RunAsync()
        {
            try
            {
                while (true)
                {
                    await Console.Out.WriteLineAsync("Waiting for request...").ConfigureAwait(false);

                    var result = await this.listener.ReceiveAsync().ConfigureAwait(false);
                    await Console.Out.WriteLineAsync($"Received request from {result.RemoteEndPoint}.").ConfigureAwait(false);

                    var now = DateTimeOffset.Now;
                    var content = ToBytes(now);
                    await this.listener.SendAsync(content, content.Length, result.RemoteEndPoint).ConfigureAwait(false);
                    await Console.Out.WriteLineAsync($"Sent response {now:R} to {result.RemoteEndPoint}.").ConfigureAwait(false);
                }
            }
            catch (SocketException e)
            {
                await Console.Error.WriteLineAsync(e.Message).ConfigureAwait(false);
            }
            finally
            {
                this.listener.Close();
            }
        }

        /// <inheritdoc cref="IDisposable"/>
        public void Dispose()
        {
            this.listener?.Close();
            this.listener?.Dispose();
        }
    }
}