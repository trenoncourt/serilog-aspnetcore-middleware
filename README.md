# serilog-aspnetcore-middleware &middot; [![NuGet](https://img.shields.io/nuget/v/Serilog.Aspnetcore.Middleware.svg?style=flat-square)](https://www.nuget.org/packages/Serilog.Aspnetcore.Middleware) [![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com) [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://github.com/trenoncourt/serilog-aspnetcore-middleware/blob/master/LICENSE)

> A middleware for aspnetcore to log httpcontext data with serilog.

## Installation

```powershell
Install-Package Serilog.Aspnetcore.Middleware
```

## Usage example

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
  app.UseHttpContextLogger();
  ...
}
```

You'll have a log event per request with all HttpContext informations including request & response body

![alt text](https://raw.githubusercontent.com/trenoncourt/serilog-aspnetcore-middleware/master/samples/Serilog.Aspnetcore.Middleware.Sample/console.png)

## Settings

Soon...

## Contributing

1. Fork it
2. Create your feature branch (`git checkout -b feature/fooBar`)
3. Commit your changes (`git commit -am 'Add some fooBar'`)
4. Push to the branch (`git push origin feature/fooBar`)
5. Create a new Pull Request
