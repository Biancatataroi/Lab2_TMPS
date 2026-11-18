# TMPS Lab 2 — Student Grade Management System

This project extends the original Lab 1 C# console application (Singleton, Builder and Factory Method), retaining the existing menu, models and reports.

## Structural patterns
- **Decorator:** `ReportDecorator`, `HeaderDecorator`, `HonorsDecorator` wrap `IAcademicReport` to add optional information.
- **Proxy:** `ReportProxy` implements `IAcademicReport`; it blocks report generation for students without grades and forwards valid requests unchanged.
- **Facade:** `AcademicReportFacade` coordinates student lookup, Factory Method report creation, optional decorators and the proxy in one call.

Pattern interaction: `Program -> Facade -> Proxy -> Decorators -> real report`.

## Requirements
.NET 10 SDK.

## Run
```bash
dotnet build
dotnet run
```

Menu option **7** generates reports and offers optional header and honors classification. All original Lab 1 menu options remain.

## Automated tests
```bash
dotnet run -- --test
```

Six automated checks, two for each pattern, are included. The command exits with a nonzero exit code if a test fails. No third-party testing packages required.

## Structure
- `Program.cs`: menu / client code
- `Domain/GradeSystem.cs`: Student, GradeManager and grade calculators
- `Creational/`: unchanged Lab 1 Singleton, Builder, Factory Method and report classes
- `Structural/`: the three new structural pattern implementations
- `Tests/PatternTests.cs`: six checks
- `REPORT.md`: class diagrams, explanations, snippets and test instructions

Data resets when the application restarts. The classification thresholds used for the demo are not official university grading rules.
