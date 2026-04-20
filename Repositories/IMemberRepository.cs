using PersoneApi.Models;

namespace PersoneApi.Repositories;

public interface IMemberRepository
{
    Task<List<Member>> GetAllAsync();

    Task<Member?> GetByIdAsync(string id);

    Task<Member> CreateAsync(Member member);

    Task<bool> UpdateAsync(string id, Member member);

    Task<bool> DeleteAsync(string id);
}
