using BlazorApp1.Components;
using BlazorApp1.Components.Auth;
using BlazorApp1.Components.Services.ClientImplementations;
using BlazorApp1.Components.Services.ClientInterfaces;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication()
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        // other cookie settings
    });

// Add other services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7279")
});

builder.Services.AddScoped<IPostService, HttpPostService>();
builder.Services.AddScoped<IUserService, HttpUserService>();
builder.Services.AddScoped<ICommentService, HttpCommentService>();
builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

// Enable authentication middleware
app.UseAuthentication();  // This line is necessary to apply authentication globally

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();