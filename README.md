# Rexy's Haunted Bakery

 [Godot](https://godotengine.org/) in the C# language.

## Downloading the repository

This repository uses [git lfs](https://git-lfs.github.com/) to better store large files (such as images and sounds), so start by installing it on your computer.

Then, you can clone the repository with
```
git lfs clone git@github.com:Swynfel/rexy-haunted-bakery.git
```

## Dependencies

To build and run the project, you will need:
- [Godot 3.2.3 - Mono version](https://godotengine.org/download)
- [.NET Core 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)

### Godot
Start by installing [Godot 3.2.3 - Mono version](https://godotengine.org/download) directly on the official website.

Make sure you take the **Mono** version, and the correct 64-bit / 32-bit depending on your architecture.

### .Net Core
Although Godot mentions installing *MSBuild*, for this project it is recommended to install the [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1). On linux, a simple `apt-get install dotnet-sdk-3.1` does the trick.

You should be able to call the following command with no error, and see a line mentioning the `v3.1` sdk appear.
```
dotnet --list-sdks
```

(Note: At first I wanted to use [.NET Core 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) for C# 9's latest functionalities, but there seems to be some issues during export, so I'll have to wait for it to be officially release and better by Godot.)

## Build and Run
Open the Godot editor, and select the project.
Press the run icon (triangle in top-right corner), or press F5.

If Godot has trouble finding the *.NET Core* SDK, go in 
*Editor/"Editor Settings..."/Mono/Builds/"Build Tool"* and check that the correct tool is selected (probably *dotnet CLI*)


## Development

### Omnisharp

There is a `omnisharp.json` file to handle the code formatting conventions.