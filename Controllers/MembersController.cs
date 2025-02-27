
using Microsoft.AspNetCore.Mvc;
using MemberService.Models.DTOs;
using MemberService.Services.Interfaces;

namespace MemberService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllMembers()
        {
            var members = await _memberService.GetAllMembersAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetMemberById(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);

            if (member == null)
                return NotFound();

            return Ok(member);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<MemberDto>> GetMemberByEmail(string email)
        {
            var member = await _memberService.GetMemberByEmailAsync(email);

            if (member == null)
                return NotFound();

            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult<MemberDto>> CreateMember(CreateMemberDto createMemberDto)
        {
            try
            {
                var newMember = await _memberService.CreateMemberAsync(createMemberDto);
                return CreatedAtAction(nameof(GetMemberById), new { id = newMember.Id }, newMember);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MemberDto>> UpdateMember(int id, UpdateMemberDto updateMemberDto)
        {
            try
            {
                var updatedMember = await _memberService.UpdateMemberAsync(id, updateMemberDto);

                if (updatedMember == null)
                    return NotFound();

                return Ok(updatedMember);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMember(int id)
        {
            var result = await _memberService.DeleteMemberAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}