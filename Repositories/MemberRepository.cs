using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PersoneApi.Models;
using PersoneApi.Settings;

namespace PersoneApi.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly IMongoCollection<Member> _members;

    public MemberRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbOptions)
    {
        var settings = mongoDbOptions.Value;
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _members = database.GetCollection<Member>(settings.MemberCollectionName);
    }

    public async Task<List<Member>> GetAllAsync()
    {
        return await _members.Find(_ => true).ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(string id)
    {
        return await _members.Find(member => member.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Member> CreateAsync(Member member)
    {
        await _members.InsertOneAsync(member);
        return member;
    }

    public async Task<bool> UpdateAsync(string id, Member member)
    {
        member.Id = id;
        var result = await _members.ReplaceOneAsync(existing => existing.Id == id, member);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _members.DeleteOneAsync(member => member.Id == id);
        return result.DeletedCount > 0;
    }
}
