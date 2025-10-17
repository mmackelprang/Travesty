# Travesty Generator - Developer Notes

## Project Structure

```
TravestyGenerator/
├── Program.cs                    # Main entry point and CLI argument parsing
├── TravestyTextGenerator.cs      # Core Markov chain algorithm implementation
└── TravestyGenerator.csproj      # Project file
```

## Core Algorithm

The `TravestyTextGenerator` class implements the Markov chain text generation algorithm:

1. **AnalyzeText()**: Builds a dictionary mapping n-grams to their possible next characters
2. **Generate()**: Uses the dictionary to probabilistically generate new text

## Building

```bash
dotnet build
```

## Running

```bash
dotnet run -- [options]
```

## Publishing

To create a standalone executable:

```bash
dotnet publish -c Release -r win-x64 --self-contained
dotnet publish -c Release -r linux-x64 --self-contained
dotnet publish -c Release -r osx-x64 --self-contained
```

## Code Style

- Uses C# 10+ features (implicit usings, nullable reference types)
- Follows standard .NET conventions
- Well-documented with XML comments
