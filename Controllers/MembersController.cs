using Microsoft.AspNetCore.Mvc;
using MemberService.Models.DTOs;
using MemberService.Services.Interfaces;

namespace MemberService.Controllers
{
    // Indicates that this controller responds to web API requests.
    [ApiController]
    // Sets the base route for all actions in this controller to "api/Members".
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        // Private field to hold the member service dependency.
        private readonly IMemberService _memberService;

        // Constructor with dependency injection for IMemberService.
        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: api/Members
        // Retrieves a list of all members.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllMembers()
        {
            // Asynchronously fetch all members using the member service.
            var members = await _memberService.GetAllMembersAsync();
            // Return HTTP 200 OK with the list of members.
            return Ok(members);
        }

        // GET: api/Members/{id}
        // Retrieves a specific member by its unique identifier.
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetMemberById(int id)
        {
            // Fetch the member by ID using the member service.
            var member = await _memberService.GetMemberByIdAsync(id);

            // If the member does not exist, return HTTP 404 Not Found.
            if (member == null)
                return NotFound();

            // Return HTTP 200 OK with the member data.
            return Ok(member);
        }

        // GET: api/Members/email/{email}
        // Retrieves a member by their email address.
        [HttpGet("email/{email}")]
        public async Task<ActionResult<MemberDto>> GetMemberByEmail(string email)
        {
            // Fetch the member by email using the member service.
            var member = await _memberService.GetMemberByEmailAsync(email);

            // If no member is found, return HTTP 404 Not Found.
            if (member == null)
                return NotFound();

            // Return HTTP 200 OK with the member data.
            return Ok(member);
        }

        // POST: api/Members
        // Creates a new member using the provided data.
        [HttpPost]
        public async Task<ActionResult<MemberDto>> CreateMember(CreateMemberDto createMemberDto)
        {
            try
            {
                // Call the service to create a new member.
                var newMember = await _memberService.CreateMemberAsync(createMemberDto);
                // Return HTTP 201 Created, along with the route to get the newly created member.
                return CreatedAtAction(nameof(GetMemberById), new { id = newMember.Id }, newMember);
            }
            // If the member creation fails due to business rules, return a Bad Request.
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Members/{id}
        // Updates an existing member with new data.
        [HttpPut("{id}")]
        public async Task<ActionResult<MemberDto>> UpdateMember(int id, UpdateMemberDto updateMemberDto)
        {
            try
            {
                // Call the service to update the member with the specified ID.
                var updatedMember = await _memberService.UpdateMemberAsync(id, updateMemberDto);

                // If the member is not found, return HTTP 404 Not Found.
                if (updatedMember == null)
                    return NotFound();

                // Return HTTP 200 OK with the updated member data.
                return Ok(updatedMember);
            }
            // If the update operation fails due to business rules, return a Bad Request.
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Members/{id}
        // Deletes a member specified by their unique identifier.
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMember(int id)
        {
            // Call the service to delete the member.
            var result = await _memberService.DeleteMemberAsync(id);

            // If deletion is unsuccessful (e.g., member not found), return HTTP 404 Not Found.
            if (!result)
                return NotFound();

            // Return HTTP 204 No Content to indicate successful deletion.
            return NoContent();
        }
    }
}
