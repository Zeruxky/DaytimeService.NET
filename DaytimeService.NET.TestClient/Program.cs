// <copyright file="Program.cs" author="Philip 'Zeruxky' Wille">
// Copyright (c) Philip 'Zeruxky' Wille. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DaytimeService.NET.TestClient
{
    using System;
    using System.Threading.Tasks;
    using CommandLine;

    /// <summary>
    /// Provides the main entry point to start the <see cref="TestClient"/> through the CLI.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments to configure the <see cref="TestClient"/>.</param>
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

            await parser.ParseArguments<ClientArguments>(args).WithParsedAsync(TestClient.RunAsync).ConfigureAwait(false);
        }
    }
}
