using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PersoneApi.Models;
using PersoneApi.Settings;

namespace PersoneApi.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IMongoCollection<Person> _people;

    public PersonRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbOptions)
    {
        var settings = mongoDbOptions.Value;
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _people = database.GetCollection<Person>("people");
    }

    public async Task<List<Person>> GetAllAsync()
    {
        return await _people.Find(_ => true).ToListAsync();
    }

    public async Task<Person?> GetByIdAsync(string id)
    {
        return await _people.Find(person => person.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Person> CreateAsync(Person person)
    {
        await _people.InsertOneAsync(person);
        return person;
    }

    public async Task<bool> UpdateAsync(string id, Person person)
    {
        person.Id = id;
        var result = await _people.ReplaceOneAsync(existing => existing.Id == id, person);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _people.DeleteOneAsync(person => person.Id == id);
        return result.DeletedCount > 0;
    }
}
