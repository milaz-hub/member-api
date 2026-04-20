using PersoneApi.Dtos;

namespace PersoneApi.Services;

public interface IPersonService
{
    Task<List<PersonDto>> GetAllAsync();

    Task<PersonDto?> GetByIdAsync(string id);

    Task<PersonDto> CreateAsync(PersonCreateDto createDto);

    Task<bool> UpdateAsync(string id, PersonUpdateDto updateDto);

    Task<bool> DeleteAsync(string id);
}
