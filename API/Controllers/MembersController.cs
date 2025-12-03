using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace zsm.Controllers;

[Route("zsm/[controller]")] // localhost:5001/zsm/members
[ApiController]
public class MembersController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
    {
        var members = await context.Users.ToListAsync();
        return members;
    }

    [HttpGet("{id}")]   // localhost:5001/zsm/members/bob-id
    public async Task<ActionResult<AppUser>> GetMember(string id)
    {
        var member = await context.Users.FindAsync(id);
        if (member == null)
            return NotFound();  // 返回 404
        return member;
    }
}