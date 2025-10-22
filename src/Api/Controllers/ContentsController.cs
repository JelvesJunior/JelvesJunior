using System.IdentityModel.Tokens.Jwt;
using Application.DTOs.Content;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentsController : ControllerBase
{
    private readonly IContentService _contentService;

    public ContentsController(IContentService contentService)
    {
        _contentService = contentService;
    }

    /// <summary>
    /// Creates content. Requires Creator role.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "CreatorOnly")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateContentDto dto, CancellationToken cancellationToken)
    {
        var creatorId = GetUserId();
        var id = await _contentService.CreateAsync(creatorId, dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    /// <summary>
    /// Gets content by id.
    /// </summary>
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ContentDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var content = await _contentService.GetByIdAsync(id, cancellationToken);
        if (content is null)
        {
            return NotFound();
        }

        return Ok(content);
    }

    /// <summary>
    /// Searches content.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<ContentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] int? creatorId, [FromQuery] string? q, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await _contentService.SearchAsync(creatorId, q, page, pageSize, cancellationToken);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result.Items);
    }

    /// <summary>
    /// Updates content. Requires Creator role.
    /// </summary>
    [HttpPut("{id:int}")]
    [Authorize(Policy = "CreatorOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateContentDto dto, CancellationToken cancellationToken)
    {
        await _contentService.UpdateAsync(id, GetUserId(), dto, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Deletes content. Requires Creator role.
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize(Policy = "CreatorOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _contentService.DeleteAsync(id, GetUserId(), cancellationToken);
        return NoContent();
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (userIdClaim is null || !int.TryParse(userIdClaim, out var id))
        {
            throw new InvalidOperationException("User identifier not found in token.");
        }

        return id;
    }
}
