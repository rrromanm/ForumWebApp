using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string _filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }
    
    private async Task<List<User>> LoadUsersAsync()
    {
        string usersAsJson = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<User>>(usersAsJson);
    }

    private async Task SaveUsersAsync(List<User> users)
    {
        string usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(_filePath, usersAsJson);
    }

    public async Task<User> AddAsync(User user)
    {
        List<User> users = await LoadUsersAsync();
        user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);
        await SaveUsersAsync(users);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        List<User> users = await LoadUsersAsync();
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }
        users.Remove(existingUser);
        users.Add(user);
        await SaveUsersAsync(users);
    }

    public async Task DeleteAsync(int id)
    {
        List<User> users = await LoadUsersAsync();
        User? userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        users.Remove(userToRemove);
        await SaveUsersAsync(users);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        List<User> users = await LoadUsersAsync();
        User? user = users.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        return user;
    }

    public IQueryable<User> GetMany()
    {
        string usersAsJson = File.ReadAllText(_filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        return users.AsQueryable();
    }
}