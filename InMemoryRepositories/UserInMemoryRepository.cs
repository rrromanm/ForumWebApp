using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    List<User> users = new List<User>();

    public UserInMemoryRepository()
    {
        _ = AddAsync(new User("admin", "admin")).Result;
        _ = AddAsync(new User("jane_smith", "securepass456")).Result;
        _ = AddAsync(new User("jhon_doe", "pass1234")).Result;
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