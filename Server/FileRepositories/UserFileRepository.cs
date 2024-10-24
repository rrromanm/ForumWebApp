using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string FilePath = "users.json";

    public UserFileRepository()
    {
        try
        {
            if (!File.Exists(FilePath)) 
            { 
                File.WriteAllText(FilePath, "[]"); 
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Error creating the file: {ex.Message}");
        }
        
    }
    
    private async Task<List<User>> ReadData()
    {
            string usersAsJson = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();
    }


    private async Task WriteData(List<User> users)
    {
        string usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(FilePath, usersAsJson);
    }


    public async Task<User> AddAsync(User user)
    {
        List<User> users = await ReadData(); 
        user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);
        await WriteData(users);
        return user;
    }



    public async Task UpdateAsync(User user)
    {
        List<User> users = await ReadData();
        User? existingUser = users.SingleOrDefault(c => c.Id == user.Id);
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
        => ReadData().Result.AsQueryable();

        public async Task<User> GetByUsernameAsync(string username)
        {
            List<User> users = await ReadData();
            User? user = users.SingleOrDefault(u => u.Username == username);
            if (user is null)
            {
                throw new InvalidOperationException($"User with ID '{username}' not found");
            }
            return user;
        }
}