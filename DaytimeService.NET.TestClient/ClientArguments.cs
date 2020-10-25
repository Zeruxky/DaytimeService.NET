// <copyright file="ClientArguments.cs" author="Philip 'Zeruxky' Wille">
// Copyright (c) Philip 'Zeruxky' Wille. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DaytimeService.NET.TestClient
{
    using System.Net.Sockets;
    using CommandLine;

    /// <summary>
    /// Represents the available options to configure the <see cref="TestClient"/>.
    /// </summary>
    public class ClientArguments
    {
        /// <summary>
        /// Gets or sets the source port of the <see cref="TestClient"/>.
        /// </summary>
        /// <value>
        /// The source port of the <see cref="TestClient"/>.
        /// </value>
        /// <remarks>
        /// If no port is given, the port will be set to -1 and a random port will be chosen.
        /// </remarks>
        [Option('s', "src-port", Default = -1, HelpText = "Set the source port of the client. If nothing is set, a free random port will be used.")]
        public int SourcePort { get; set; }

        /// <summary>
        /// Gets or sets the destination port. This is equal to the listening port of the daytime service.
        /// </summary>
        /// <value>
        /// The port of the daytime service.
        /// </value>
        /// <remarks>
        /// Default the port 13 will be chosen for TCP and UDP according to RFC 867.
        /// </remarks>
        [Option('d', "desc-port", Default = (int)13, HelpText = "Set the port of the to calling daytime service. Default will be used the port 13.")]
        public int DestinationPort { get; set; }

        /// <summary>
        /// Gets or sets the mode of the connection between the <see cref="TestClient"/> and the daytime service.
        /// </summary>
        /// <value>
        /// The connection mode.
        /// </value>
        /// <remarks>
        /// The only two available modes are UDP and TCP.
        /// </remarks>
        [Option('c', "connection-type", Default = ProtocolType.Udp, HelpText = "Sets the connection type of the client. The type should match with that of the day time service.")]
        public ProtocolType Mode { get; set; }
    }
}