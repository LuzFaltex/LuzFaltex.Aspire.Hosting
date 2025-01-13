# LuzFaltex.Aspire.Hosting.Zitadel

This package allows for using Zitadel as the identity provider for your Aspire host.

## How To Use

### Installing the package

In your AppHost project, install the .NET Aspire Zitadel Hosting library with [NuGet](https://www.nuget.org/):

```
dotnet add package LuzFaltex.Aspire.Hosting.Zitadel
```

### Usage Example

Then, in the *Program.cs* file of `AppHost`, add a Zitadel resource and enable service discovery using the following methods:

```cs
var zitadel = builder.AddZitadel("zitadel", 8080);

var myService = builder.AddProject<Projects.MyService>)
                       .WithReference(zitadel);
```

**Recommendation**: For local development use a stable port for the Zitadel resource (8080 in the example above). It can be any port, but it should be stable to avoid issues with browser cookies that will persist OIDC tokens (which include the authority url, with port) beyond the lifetime of the AppHost.

## Feedback & Contributing

https://github.com/LuzFaltex/LuzFaltex.Aspire.Hosting