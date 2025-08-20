using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClassAct.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register HttpClient for DI with the base address of the host environment
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register StudentService for DI
builder.Services.AddScoped<StudentService>();

// Register CourseService for DI
builder.Services.AddScoped<CourseService>();

await builder.Build().RunAsync();
