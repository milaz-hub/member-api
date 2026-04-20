using PersoneApi.Dtos;
using PersoneApi.Models;
using PersoneApi.Repositories;
using MongoDB.Bson;

namespace PersoneApi.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<List<MemberDto>> GetAllAsync()
    {
        var members = await _memberRepository.GetAllAsync();
        return members.Select(MapToDto).ToList();
    }

    public async Task<MemberDto?> GetByIdAsync(string id)
    {
        if (!IsValidObjectId(id))
        {
            return null;
        }

        var member = await _memberRepository.GetByIdAsync(id);
        return member is null ? null : MapToDto(member);
    }

    public async Task<MemberDto> CreateAsync(MemberCreateDto createDto)
    {
        var member = new Member
        {
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Email = createDto.Email,
            DateOfBirth = createDto.DateOfBirth
        };

        var createdMember = await _memberRepository.CreateAsync(member);
        return MapToDto(createdMember);
    }

    public async Task<bool> UpdateAsync(string id, MemberUpdateDto updateDto)
    {
        if (!IsValidObjectId(id))
        {
            return false;
        }

        var member = new Member
        {
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName,
            Email = updateDto.Email,
            DateOfBirth = updateDto.DateOfBirth
        };

        return await _memberRepository.UpdateAsync(id, member);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        if (!IsValidObjectId(id))
        {
            return false;
        }

        return await _memberRepository.DeleteAsync(id);
    }

    private static bool IsValidObjectId(string id)
    {
        return ObjectId.TryParse(id, out _);
    }

    private static MemberDto MapToDto(Member member)
    {
        return new MemberDto
        {
            Id = member.Id ?? string.Empty,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Email = member.Email,
            DateOfBirth = member.DateOfBirth
        };
    }
}
