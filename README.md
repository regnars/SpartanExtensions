# Project Title

Swiss knife for .Net developer

### Prerequisites

To make NuGet packages you need the nuget.exe

## Tests

Currently there are only UnitTests. Howerver you should run them before you ask for a pull-request.

### Code style

We are using Resharper and sticking to it's default settings

## Deployment

This library is available as [NuGet package](https://www.nuget.org/packages/SpartanExtensions/)
To test NuGet package you can make a local NuGet feed on your machine. [Here](https://docs.microsoft.com/en-us/nuget/hosting-packages/local-feeds) is short description on how to do that.
NuGet.CommandLine NuGet package is added to projects dependencies and should be available for use in PackageManagerConsole when dependencies are resolved.

Some usefull nuget.exe commands:

`nuget pack .\SpartanExtensions\SpartanExtensions.csproj -Prop Configuration=Release`
`nuget add SpartanExtensions.X.X.XXXX.XXXXX.nupkg -source \\your\nuget\feed`

## Authors

* **Regnars** - *Initial work* - [regnars](https://github.com/regnars)

See also the list of [contributors](https://github.com/regnars/SpartanExtensions/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License

## Acknowledgments

Jet to come... Be first to enter this list ;)