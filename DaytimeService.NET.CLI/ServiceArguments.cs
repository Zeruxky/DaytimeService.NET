// <copyright file="ServiceArguments.cs" author="Philip 'Zeruxky' Wille">
// Copyright (c) Philip 'Zeruxky' Wille. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DaytimeService.NET.CLI
{
    using System.Net.Sockets;
    using CommandLine;

    /// <summary>
    /// Defines the available argument options to configure the daytime service.
    /// </summary>
    public class ServiceArguments
    {
        /// <summary>
        /// Gets or sets the mode of the daytime service (UDP or TCP).
        /// </summary>
        /// <value>
        /// The mode of the daytime service.
        /// </value>
        /// <remarks>
        /// The default mode is UDP.
        /// </remarks>
        [Option('c', "connection-type", Default = ProtocolType.Udp, Required = true, HelpText = "Sets the connection type of the daytime service.")]
        public ProtocolType Mode { get; set; }

        /// <summary>
        /// Gets or sets the port on which the daytime service should listen.
        /// </summary>
        /// <value>
        /// The listening port.
        /// </value>
        /// <remarks>
        /// The default port of the daytime service is 13 for both UDP and TCP according to RFC 867.
        /// </remarks>
        [Option('s', "src-port", Default = 13, HelpText = "Sets the listening port of the daytime service.")]
        public int Port { get; set; }
    }
}