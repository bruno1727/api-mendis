# API Mendis

A .NET 7.0 Web API project for travel recommendations and suggestions.

## Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [VS Code](https://code.visualstudio.com/)
- [C# Extension for VS Code](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

## Getting Started with VS Code

1. Open the project in VS Code
2. Install the recommended extensions when prompted
3. Press `F5` to run the project with debugging, or use Ctrl+Shift+P and run "Tasks: Run Task" → "build" to build the project

## Available Tasks

- **Build**: `Ctrl+Shift+P` → "Tasks: Run Task" → "build"
- **Watch**: `Ctrl+Shift+P` → "Tasks: Run Task" → "watch" (runs with hot reload)
- **Test**: `Ctrl+Shift+P` → "Tasks: Run Task" → "test"
- **Publish**: `Ctrl+Shift+P` → "Tasks: Run Task" → "publish"

## Running the API

### Using VS Code
- Press `F5` to start debugging
- The API will be available at `https://localhost:5001` and `http://localhost:5000`

### Using Terminal
```bash
cd ApiMendis
dotnet run
```

### Using Watch Mode (Hot Reload)
```bash
cd ApiMendis
dotnet watch run
```

## Running Tests

### Using VS Code
- Use `Ctrl+Shift+P` → "Tasks: Run Task" → "test"

### Using Terminal
```bash
dotnet test ApiMendisTests/ApiMendisTests.csproj
```

## Project Structure

- `ApiMendis/` - Main API project
- `ApiMendisTests/` - Unit tests project
- `.vscode/` - VS Code configuration files
  - `launch.json` - Debug configuration
  - `tasks.json` - Build tasks
  - `extensions.json` - Recommended extensions

## Development

The project is now optimized for VS Code development with:
- IntelliSense support
- Debugging configuration
- Build tasks
- Test runner integration
- Extension recommendations
