using PersoneApi.Dtos;
using PersoneApi.Models;
using PersoneApi.Repositories;
using MongoDB.Bson;

namespace PersoneApi.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<List<PersonDto>> GetAllAsync()
    {
        var people = await _personRepository.GetAllAsync();
        return people.Select(MapToDto).ToList();
    }

    public async Task<PersonDto?> GetByIdAsync(string id)
    {
        if (!IsValidObjectId(id))
        {
            return null;
        }

        var person = await _personRepository.GetByIdAsync(id);
        return person is null ? null : MapToDto(person);
    }

    public async Task<PersonDto> CreateAsync(PersonCreateDto createDto)
    {
        var person = new Person
        {
            Name = createDto.Name,
            Surname = createDto.Surname,
            Email = createDto.Email,
            DateOfBirth = createDto.DateOfBirth
        };

        var createdPerson = await _personRepository.CreateAsync(person);
        return MapToDto(createdPerson);
    }

    public async Task<bool> UpdateAsync(string id, PersonUpdateDto updateDto)
    {
        if (!IsValidObjectId(id))
        {
            return false;
        }

        var person = new Person
        {
            Name = updateDto.Name,
            Surname = updateDto.Surname,
            Email = updateDto.Email,
            DateOfBirth = updateDto.DateOfBirth
        };

        return await _personRepository.UpdateAsync(id, person);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        if (!IsValidObjectId(id))
        {
            return false;
        }

        return await _personRepository.DeleteAsync(id);
    }

    private static bool IsValidObjectId(string id)
    {
        return ObjectId.TryParse(id, out _);
    }

    private static PersonDto MapToDto(Person person)
    {
        return new PersonDto
        {
            Id = person.Id ?? string.Empty,
            Name = person.Name,
            Surname = person.Surname,
            Email = person.Email,
            DateOfBirth = person.DateOfBirth
        };
    }
}
