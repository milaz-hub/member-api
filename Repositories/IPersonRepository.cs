using PersoneApi.Models;

namespace PersoneApi.Repositories;

public interface IPersonRepository
{
    Task<List<Person>> GetAllAsync();

    Task<Person?> GetByIdAsync(string id);

    Task<Person> CreateAsync(Person person);

    Task<bool> UpdateAsync(string id, Person person);

    Task<bool> DeleteAsync(string id);
}
