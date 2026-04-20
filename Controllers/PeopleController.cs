using Microsoft.AspNetCore.Mvc;
using PersoneApi.Dtos;
using PersoneApi.Services;

namespace PersoneApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IPersonService _personService;

    public PeopleController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PersonDto>>> GetAllAsync()
    {
        var people = await _personService.GetAllAsync();
        return Ok(people);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetByIdAsync(string id)
    {
        var person = await _personService.GetByIdAsync(id);

        if (person is null)
        {
            return NotFound();
        }

        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<PersonDto>> CreateAsync([FromBody] PersonCreateDto createDto)
    {
        var createdPerson = await _personService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = createdPerson.Id }, createdPerson);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] PersonUpdateDto updateDto)
    {
        var updated = await _personService.UpdateAsync(id, updateDto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var deleted = await _personService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
