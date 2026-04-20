using Microsoft.AspNetCore.Mvc;
using PersoneApi.Dtos;
using PersoneApi.Services;

namespace PersoneApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet()]
    [ProducesResponseType(typeof(List<MemberDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<MemberDto>>> GetAllAsync()
    {
        var members = await _memberService.GetAllAsync();
        return Ok(members);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MemberDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberDto>> GetById(string id)
    {
        var member = await _memberService.GetByIdAsync(id);

        if (member is null)
        {
            return NotFound();
        }

        return Ok(member);
    }

    [HttpPut()]
    [ProducesResponseType(typeof(MemberDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<MemberDto>> CreateAsync([FromBody] MemberCreateDto createDto)
    {
        var createdMember = await _memberService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = createdMember.Id }, createdMember);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] MemberUpdateDto updateDto)
    {
        var updated = await _memberService.UpdateAsync(id, updateDto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var deleted = await _memberService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
