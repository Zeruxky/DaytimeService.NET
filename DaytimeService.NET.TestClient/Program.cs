using System;
using System.Threading.Tasks;
using CommandLine;

namespace DaytimeService.NET.TestClient
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

            await parser.ParseArguments<ClientArguments>(args).WithParsedAsync(TestClient.RunAsync).ConfigureAwait(false);
        }
    }
}
