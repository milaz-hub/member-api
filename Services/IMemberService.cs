using PersoneApi.Dtos;

namespace PersoneApi.Services;

public interface IMemberService
{
    Task<List<MemberDto>> GetAllAsync();

    Task<MemberDto?> GetByIdAsync(string id);

    Task<MemberDto> CreateAsync(MemberCreateDto createDto);

    Task<bool> UpdateAsync(string id, MemberUpdateDto updateDto);

    Task<bool> DeleteAsync(string id);
}
