# AI Girlfriend
## For Grade 12 Computer Science
This is an AI anime girlfriend whose hair changes color based on her mood. It's very epic and cool.

*Note for teacher: Much of the project is just a template. See **MainWindow.axaml.cs, MainWindow.axaml,** and **AI.cs** for the stuff I coded personally.*

## Compatibility
Works on Windows and Linux. Developed specifically on/for Fedora Linux.

## Dependencies
- .NET 8.0
-   Earlier .NET versions work too, but AI_Girlfriend.csproj needs to be updated to target earlier .NET versions.

## Installation
**Before you do anything, you must set the BAEI_KEY environment variable with an OpenAI key capable of running gpt-3.5-turbo.**
In Windows, you can set the BAEI_KEY variable with the following command:
```bat
$env:BAEI_KEY = "my key here"
```
Otherwise, if using Linux,
```bash
export BAEI_KEY=my key here
```
After setting the environment variable, run the following commands to install the program.
```bash
git clone git@github.com:LobsterRoast/CompSci_AI_Girlfriend.git
cd CompSci_AI_Girlfriend
make # If you don't have make installed, run the dotnet publish command inside the Makefile
```
After installing, run the executable in the build directory. If the exe doesn't work, open the terminal and run the following:
```bash
dotnet AI_Girlfriend.dll
```
**The Windows executable doesn't like running from file explorer for some reason, so even if you're running the .exe rather than the .dll, you must run it from the command line.**


**Note:** This targets  .NET 8.0 by default. You may need to edit AI_Girlfriend.csproj to target a different version of .NET depending on what works for you and your environment.

## Resources used:
- [Avalonia](https://avaloniaui.net/)
- [Official .NET OpenAI API](https://github.com/openai/openai-dotnet)
