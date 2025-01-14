# Arora.GlobalExceptionHandler

`Arora.GlobalExceptionHandler` is an open-source .NET 8+ class library that provides global exception handling middleware for minimal APIs. It allows you to catch unhandled exceptions across your application in a single `try-catch` block, making error handling simpler and more consistent.

## Why This Library Was Created

In modern web applications, managing errors gracefully is essential for both debugging and providing a good user experience. The typical approach to exception handling involves wrapping each endpoint or request handler in a `try-catch` block, which can be cumbersome and repetitive.

The idea behind `Arora.GlobalExceptionHandler` is to centralize exception handling in a single middleware, so that all exceptions—regardless of where they occur—are caught and handled in one place. This simplifies the code, reduces redundancy, and ensures consistent error responses for all unhandled exceptions.

## How It Works

This library adds middleware to your `WebApplication` pipeline that intercepts all requests. When an unhandled exception occurs, the middleware catches it and returns a formatted JSON response with the exception details.

By using this middleware, you ensure that your application always returns a consistent error format, no matter where the exception originates.

## Features

- **Centralized Error Handling**: Catch all exceptions in one `try-catch` block.
- **JSON Response**: Automatically formats error messages in a JSON response.
- **Minimal Configuration**: Simple and straightforward integration into your application.

## Installation

You can add the `Arora.GlobalExceptionHandler` library to your .NET 8+ class library by referencing the project or package. Ensure that your project targets .NET 8+.

## Usage

### Step 1: Install the Library

Add `Arora.GlobalExceptionHandler` to your project by adding the necessary reference in your `.csproj` file or by using a package manager.

### Step 2: Use the Middleware

In your `Program.cs` or `Startup.cs` file, call the `UseGlobalExceptionHandler` extension method to add the middleware to the pipeline.

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Arora.GlobalExceptionHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Add global exception handler middleware
app.UseGlobalExceptionHandler();

// Configure your endpoints
app.MapGet("/", () => "Hello World!");

app.Run();
```