using BlazorApp.Client.Core.Interfaces;
using BlazorApp.Client.Infrastructure.Repositories;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddMudServices();

builder.Services.AddTransient<ICommentsRepository, DudCommentsRepository>();

var app = builder.Build();
    
await app.RunAsync();
