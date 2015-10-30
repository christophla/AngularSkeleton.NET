## Motivation

The motiviation is to provide a starting point for rapidly building a full-stack
single page application leveraging .NET and Entity Framework.

## Installation

The project leverages Entity Framework code migrations to build the database. 

### From the VIsual Studio Package Manager Console

    Select the AngularSkeleton.DataAccess default project
    Run PM>Update-Database

Visual Studio 2012 may have issues automatically restoring NuGet packages on build.
Right-clicking on the solution folder will provide an option to enable restore.

## API Reference

The API documentation is embedded within the application using the Swashbuckle Swagger library

## Tests

Integration tests are provided in the AngularSkeleton.Tests.Integration library. The require the
xunit.visualstudio integration library which is loaded automatically via NuGet.


