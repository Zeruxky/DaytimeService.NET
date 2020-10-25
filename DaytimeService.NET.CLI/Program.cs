// <copyright file="Program.cs" author="Philip 'Zeruxky' Wille">
// Copyright (c) Philip 'Zeruxky' Wille. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DaytimeService.NET.CLI
{
    using System;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using CommandLine;

    /// <summary>
    /// Provides the main entry point to start the daytime service through the CLI.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous main operation.</returns>
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
            if (arguments.Mode == ProtocolType.Tcp)
            {
                using (var service = new TcpDaytimeService(arguments.Port))
                {
                    await service.RunAsync().ConfigureAwait(false);
                }
            }

            if (arguments.Mode == ProtocolType.Udp)
            {
                using (var service = new UdpDaytimeService(arguments.Port))
                {
                    await service.RunAsync().ConfigureAwait(false);
                }
            }

            throw new ArgumentException($"Unknown connection type {arguments.Mode}.");
        }
    }
}
