# C0deGeek Libraries

A collection of .NET utility libraries providing core functionality and extensions for modern C# development.

## Packages

This solution produces two NuGet packages:

### C0deGeek.Core (v1.0.1)
Core utilities and base functionality including:
- Type-safe enum implementation
- DateTime utilities with testing support
- Code/Name domain models
- Custom validation attributes

### C0deGeek.Extensions (v1.0.1)
A comprehensive collection of extension methods enhancing basic .NET types:
- Collection extensions (safe removal operations)
- Decimal extensions (normalization and formatting)
- Enum extensions (description and member value handling)
- String extensions (enhanced null checks)
- Type extensions (reflection helpers)

## Features

### DateTime Utilities
- Mockable DateTime for testing
- Time zone handling
- Age calculation utilities

### Type-Safe Enums
- Strongly-typed enum pattern implementation
- Code/Name/Description support
- Validation and comparison capabilities

### Validation Attributes
- Enum validation (codes, descriptions, member values)
- Integration with System.ComponentModel.DataAnnotations

### Extension Methods
- Enhanced LINQ-style operations
- Formatting and conversion utilities
- Reflection helpers

## Installation

Add the NuGet packages to your project:

```bash
dotnet add package C0deGeek.Core
dotnet add package C0deGeek.Extensions
```

## Usage Examples

### DateTime Utilities
```csharp
// Set a fixed time for testing
GeekDateTime.SetUtcDateTime(new DateTime(2024, 1, 1));

// Get current time (mockable)
var now = GeekDateTime.UtcNow;

// Calculate years passed
var years = GeekDateTime.YearsPassed(startDate, endDate);
```

### Type-Safe Enums
```csharp
public class Status : TypeSafeEnumBase<Status>
{
    public static readonly Status Active = new("ACTIVE", "Active");
    public static readonly Status Inactive = new("INACTIVE", "Inactive");

    private Status(string code, string name) : base(code, name) { }
}
```

### Extensions
```csharp
// Collection extensions
collection.RemoveAll(x => x.IsObsolete);

// Decimal formatting
decimal value = 123.4500m;
string normalized = value.ToNormalizedString(); // "123.45"

// Enhanced null checks
if (str.IsNullOrEmptyOrWhiteSpace()) { }
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License
  
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.MD) file for details.