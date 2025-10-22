using System.IdentityModel.Tokens.Jwt;
using Application.DTOs.Playlists;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylistService _playlistService;

    public PlaylistsController(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }

    /// <summary>
    /// Creates a playlist. Requires Creator role.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "CreatorOnly")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreatePlaylistDto dto, CancellationToken cancellationToken)
    {
        var id = await _playlistService.CreateAsync(GetUserId(), dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    /// <summary>
    /// Gets a playlist by id.
    /// </summary>
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PlaylistDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var playlist = await _playlistService.GetByIdAsync(id, cancellationToken);
        if (playlist is null)
        {
            return NotFound();
        }

        return Ok(playlist);
    }

    /// <summary>
    /// Searches playlists.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<PlaylistDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] int? creatorId, [FromQuery] string? q, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var query = new PlaylistQuery
        {
            CreatorId = creatorId,
            Search = q,
            Page = page,
            PageSize = pageSize
        };

        var result = await _playlistService.SearchAsync(query, cancellationToken);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result.Items);
    }

    /// <summary>
    /// Updates a playlist. Requires Creator role.
    /// </summary>
    [HttpPut("{id:int}")]
    [Authorize(Policy = "CreatorOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePlaylistDto dto, CancellationToken cancellationToken)
    {
        await _playlistService.UpdateAsync(id, GetUserId(), dto, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Deletes a playlist. Requires Creator role.
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize(Policy = "CreatorOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _playlistService.DeleteAsync(id, GetUserId(), cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Adds an item to a playlist. Requires Creator role.
    /// </summary>
    [HttpPost("{playlistId:int}/items")]
    [Authorize(Policy = "CreatorOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddItem(int playlistId, [FromBody] AddPlaylistItemDto dto, CancellationToken cancellationToken)
    {
        await _playlistService.AddItemAsync(playlistId, GetUserId(), dto, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Lists items of a playlist.
    /// </summary>
    [HttpGet("{playlistId:int}/items")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<PlaylistItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetItems(int playlistId, CancellationToken cancellationToken)
    {
        var playlist = await _playlistService.GetByIdAsync(playlistId, cancellationToken);
        if (playlist is null)
        {
            return NotFound();
        }

        return Ok(playlist.Items);
    }

    /// <summary>
    /// Removes an item from a playlist. Requires Creator role.
    /// </summary>
    [HttpDelete("{playlistId:int}/items/{itemId:int}")]
    [Authorize(Policy = "CreatorOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveItem(int playlistId, int itemId, CancellationToken cancellationToken)
    {
        await _playlistService.RemoveItemAsync(playlistId, GetUserId(), itemId, cancellationToken);
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
