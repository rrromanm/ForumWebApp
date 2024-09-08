using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private List<User> users;

    public UserInMemoryRepository()
    {
        users = new List<User>
        {
            new User { Id = 1, Username = "john_doe", Password = "password123" },
            new User { Id = 2, Username = "jane_smith", Password = "securepass456" },
            new User { Id = 3, Username = "admin", Password = "adminpass789" }
        };
    }
    
    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any() ? users.Max(x => x.Id) + 1 : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(x => x.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with id {user.Id} not found");
        }
        users.Remove(existingUser);
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? userToDelete = users.SingleOrDefault(x => x.Id == id);
        if (userToDelete is null)
        {
            throw new InvalidOperationException($"User with id {id} not found");
        }
        users.Remove(userToDelete);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? userToGet = users.SingleOrDefault(p => p.Id == id);
        if (userToGet is null)
        {
            throw new InvalidOperationException($"User with ID {id} not found.");
        }
        return Task.FromResult(userToGet);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }
}