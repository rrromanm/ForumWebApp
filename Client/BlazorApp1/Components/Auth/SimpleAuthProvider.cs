using System.Security.Claims;
using System.Text.Json;
using DTOs.User;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.JSInterop;

namespace BlazorApp1.Components.Auth;

public class SimpleAuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient client;
    private readonly IJSRuntime jsRuntime;
    
    public SimpleAuthProvider(HttpClient client, IJSRuntime jsRuntime)
    {
        this.client = client;
        this.jsRuntime = jsRuntime;
    }

    public async Task Login(string username, string password)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync(
            $"https://localhost:7078/login",
            new LoginRequestDTO(username, password));
        
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(content);
            throw new Exception(content);
        }
        UserDTO userDto = JsonSerializer.Deserialize<UserDTO>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        
        string serialisedData = JsonSerializer.Serialize(userDto);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
        
        List<Claim> claims = new List<Claim>()
        {
                new Claim(ClaimTypes.Name, userDto.Username),
                new Claim("Id", userDto.Id.ToString())
        };
        
        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
        
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(claimsPrincipal))
            );
    }

    public async Task Register(string username, string password)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync(
            $"https://localhost:7078/Users",
            new AddUserDTO(username, password));
        
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(content);
            throw new Exception(content);
        }
        
        UserDTO userDto = JsonSerializer.Deserialize<UserDTO>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim("Id", userDto.Id.ToString())
        };
        
        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
        
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(claimsPrincipal))
            );
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string userAsJson = "";
        try
        {
            userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        }
        catch (InvalidOperationException e)
        {
            return new AuthenticationState(new());
        }

        if (string.IsNullOrEmpty(userAsJson))
        {
            return new AuthenticationState(new());
        } 
        UserDTO userDto = JsonSerializer.Deserialize<UserDTO>(userAsJson)!; 
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim("Id", userDto.Id.ToString())
        }; 
        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth"); 
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity); 
        return new AuthenticationState(claimsPrincipal);
    }
}