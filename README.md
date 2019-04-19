# dotnet-tutorial-app

> This is the code tutorial of a console app that connects to sqLite database.


## Running Prerequisites

* [.NET Core SDK](https://www.microsoft.com/net/download/core).
* [Online sqlite client](https://sqliteonline.com/) (if you want to view database content)

## Development Prerequisites

1. Install [Visual Studio Code](https://code.visualstudio.com/).
2. Install the [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp) for Visual Studio Code. For more information about how to install extensions on Visual Studio Code, see [VS Code Extension Marketplace](https://code.visualstudio.com/docs/editor/extension-gallery).
3. Install [NuGet Package Manager](https://marketplace.visualstudio.com/items?itemName=jmrog.vscode-nuget-package-manager) from the Visual Studio Code Extension Marketplace.
4. Install [Sqlite client VSCode extension](https://github.com/AlexCovizzi/vscode-sqlite)

## Utilities guides

* [.NET core cli guide](https://docs.microsoft.com/it-it/dotnet/core/tools/?tabs=netcore2x)
* [.NET core start with db](https://www.microsoft.com/en-us/sql-server/developer-get-started/csharp/win/step/2.html)


## How to run

* Run ```dotnet run``` on your shell window in the git cloning folder of this repo

## How to build

### Generate dll multiplatform to run with SDK

* Run ```dotnet publish -c Release -r``` on your shell window in the git cloning folder of this repo
* Under **bin\Release\netcoreapp2.2\publish** there will be your dll app
* Run in that folder ```dotnet <app-name>.dll```, in this case ```dotnet dotnetapp.dll```

### Docker container with dll multiplatform

* Run ```dotnet publish -c Release -r``` on your shell window in the git cloning folder of this repo
* Run ```docker build -t myapp .``` on your shell window in the git cloning folder of this repo
* After run ```docker run -it --rm myapp``` 

### Generate exe file

* Run ```dotnet publish -c Release -r win10-x64``` on your shell window
* Under **bin\Release\netcoreapp2.2\win10-x64\publish** there will be your exe app

The last exe file is a **distributable standalone app** executable without the .NET Core SDK.

> Here you can download the [windows db exe](https://github.com/amanganiello90/dotnet-tutorial-app/archive/distributable-db-app.zip)


## Demo
