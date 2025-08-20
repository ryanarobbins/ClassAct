# ClassAct Sample Project

This repository contains **ClassAct**, a Blazor WebAssembly sample application designed for practicing defensive programming techniques. The project simulates a simple academic management system with students, courses, and instructors.

## Purpose

- **Defensive Programming Practice:**  
  The application intentionally contains three places where users can encounter runtime errors.  
  **Your goal:** Identify and fix the issues.  Ideally, prevent people from making similar mistakes in the future.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) (Preview or latest available)

### Installation

1. Clone the repository
   
1. Open the Solution in Visual Studio

1. Create the Database Using Package Manager Console
	- Open the Package Manager Console from: Tools -> NuGet Package Manager -> Package Manager Console
	- Set the start up project to ClassAct
	- Set the default project in Package Manager Console to ClassAct.Data
	- Run the `Update-Database` command to create the database

1. OR Create the database from the command line
	- Install the dotnet ef command line tools `dotnet tool install --global dotnet-ef`
	- From the ClassAct folder that holds the folders with the projects, run `dotnet ef database update --project ClassAct.Data --startup-project ClassAct` 

1. Run the Application
   - Set the startup project to `ClassAct` and choose the `https` profile
   - Press `F5` or click on the "Start" button in Visual Studio to run in debug mode
