using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";

    public UserFileRepository()
    {
        try
        {
            if (!File.Exists(filePath)) 
            { 
                File.WriteAllText(filePath, "[]"); 
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Error creating the file: {ex.Message}");
        }
        
    }
    
    private async Task<List<User>> ReadData()
    {
            string usersAsJson = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();
    }


    private async Task WriteData(List<User> users)
    {
        string usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }


    public async Task<User> AddAsync(User user)
    {
        // Read existing users from the file
        List<User> users = await ReadData(); 
    
        // Debugging information: Check the count of users
        Console.WriteLine($"Users before adding: {users.Count}"); // Debug line

        // Assign a new ID
        user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1; 
    
        // Add the new user to the list
        users.Add(user); 
    
        // Write the updated list back to the file
        await WriteData(users); 
    
        // Confirmation message
        Console.WriteLine($"User added successfully. New User ID: {user.Id}"); // Debug line
    
        return user; // Return the new user
    }



    public async Task UpdateAsync(User user)
    {
        List<User> users = await ReadData();
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }
        users.Remove(existingUser);
        users.Add(user);
        await WriteData(users);
    }

    public async Task DeleteAsync(int id)
    {
        List<User> users = await ReadData();
        User? userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        users.Remove(userToRemove);
        await WriteData(users);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        List<User> users = await ReadData();
        User? user = users.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        return user;
    }

    public IQueryable<User> GetMany()
    {
        string usersAsJson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) !;
        return users.AsQueryable();
    }
}