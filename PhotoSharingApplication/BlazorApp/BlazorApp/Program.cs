using Microsoft.EntityFrameworkCore.Design;
using BlazorApp.Components;
using BlazorApp.Core.Interfaces;
using BlazorApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using BlazorApp.Endpoints;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();



builder.Services.AddDbContext<PhotoContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PhotoSharingDatabase"));
});

builder.Services.AddTransient<IPhotoRepository, PhotoDbRepository>();
builder.Services.AddTransient<ICommentsRepository, CommentsRepository>();

builder.Services.AddMudServices();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Configure REST API
app.MapCommentsEndpoints();

app.MapOpenApi();
app.MapScalarApiReference();



app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorApp.Client._Imports).Assembly);

app.Run();
