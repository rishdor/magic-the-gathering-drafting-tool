using magick.Components;
using magick.Services;
using magick.Models;
using Microsoft.EntityFrameworkCore;
using magick.Models.Forms;
using Microsoft.AspNetCore.Identity;
using magick.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<Service>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<DraftService>();
builder.Services.AddScoped<DeckService>();
builder.Services.AddScoped<SetService>();
builder.Services.AddScoped<CardGalleryController>();
builder.Services.AddScoped<DecksController>();

builder.Services.AddSingleton<AppState>();

builder.Services.AddDbContextFactory<MagickContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();