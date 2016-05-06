## Motivation

The motivation is to provide a starting point for rapidly building a full-stack
single page application leveraging .NET and Entity Framework.

## TypeScript

The project leverages TypeScript 1.6.  To install:
http://blogs.msdn.com/b/typescript/archive/2015/09/16/announcing-typescript-1-6.aspx

## NuGet Update

To avoid issues with xunit packages, update NuGet:
https://docs.nuget.org/consume/installing-nuget

## Azure Update

It is recommended that you update to latest version of Visual Studio Tools for Azure:

1. Open Visual Studio.
2. Go to Tools->Extensions and Updates...
3. Go to Updates->Visual Studio Gallery
4. Look for Azure SDK update to install

or download directly:
https://azure.microsoft.com/en-us/downloads/

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


