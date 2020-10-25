# DaytimeService.NET

<!--- These are examples. See https://shields.io for others or to customize this set of shields. You might want to include dependencies, project status and licence info here --->
![GitHub repo size](https://img.shields.io/github/repo-size/Zeruxky/DaytimeService.NET)
![GitHub contributors](https://img.shields.io/github/contributors/Zeruxky/DaytimeService.NET)
![GitHub stars](https://img.shields.io/github/stars/Zeruxky/DaytimeService.NET?style=social)
![GitHub forks](https://img.shields.io/github/forks/Zeruxky/DaytimeService.NET?style=social)

DaytimeService.NET is a implementation of the daytime protocol ([RFC867](https://tools.ietf.org/html/rfc867)) for C#.

It provides a console application for running the daytime server through the CLI and a test tool for triggering the daytime server. It is developed against .NET Standard 2.0.

## Prerequisites

Before you begin, ensure you have met the following requirements:
<!--- These are just example requirements. Add, duplicate or remove as required --->
* You have installed the latest version of `Microsoft Visual Studio` or `Jetbrains Rider`.
* You have installed the latest version of [`.NET Core SDK`](https://dotnet.microsoft.com/download).

## Using DaytimeService.NET

To build DaytimeService.NET, follow these steps:

* Clone this repository.
* Open the `DaytimeService.NET.sln` in `Microsoft Visual Studio` or `Jetbrains Rider`.
* Build it with `Microsoft Visual Studio`, `Jetbrains Rider` or with `dotnet build`.

For starting the server, follow these steps:

* Build the project with the before mentioned steps.
* Open PowerShell, CMD or terminal.
* Navigate to `pathToProject\DaytimeService.NET\DaytimeService.NET.CLI\bin\Debug\netcoreapp3.1` with

``` bash
cd pathToProject\DaytimeService.NET\DaytimeService.NET.CLI\bin\Debug\netcoreapp3.1
```

* Run it with

``` bash
.\DaytimeService.NET.CLI.exe -c <connection-mode> -s <listening-port>
```

You can get additional help with the `help`-command. Use it as follows:

``` bash
.\DaytimeService.NET.TestClient.exe --help
```

For starting the test client, follow these steps:

* Build the project with the before mentioned steps.
* Open PowerShell, CMD or terminal.
* Navigate to `pathToProject\DaytimeService.NET\DaytimeService.NET.TestClient\bin\Debug\netcoreapp3.1` with

``` bash
cd pathToProject\DaytimeService.NET\DaytimeService.NET.TestClient\bin\Debug\netcoreapp3.1
```

* Run it with

``` bash
.\DaytimeService.NET.TestClient.exe -c <connection-mode> -d <port-of-daytime-server> -s <source-port-of-test-client>
```

You can get additional help with the `help`-command. Use it as follows:

``` bash
.\DaytimeService.NET.TestClient.exe --help
```

## Contributing to DaytimeService.NET
<!--- If your README is long or you have some specific process or steps you want contributors to follow, consider creating a separate CONTRIBUTING.md file--->
To contribute to DaytimeService.NET, follow these steps:

1. Fork this repository.
2. Create a branch: `git checkout -b <branch_name>`.
3. Make your changes and commit them: `git commit -m '<commit_message>'`
4. Push to the original branch: `git push origin <project_name>/<location>`
5. Create the pull request.

Alternatively see the GitHub documentation on [creating a pull request](https://help.github.com/en/github/collaborating-with-issues-and-pull-requests/creating-a-pull-request).

## Dependencies

* NET.Core 3.1 for demo test client and demo server
* CommandLineParser 2.8.0
* Library is compatible to .NET Standard 2.0.

## Contributors

Thanks to the following people who have contributed to this project:

* [@Zeruxky](https://github.com/Zeruxky)

## Contact

If you want to contact me you can reach me at <philip.markus.wille@gmail.com>.

## License
<!--- If you're not sure which open license to use see https://choosealicense.com/--->

This project uses the following license: [MIT](https://github.com/Zeruxky/DaytimeService.NET/blob/main/LICENSE).
