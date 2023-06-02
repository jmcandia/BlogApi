using Microsoft.AspNetCore.Mvc;
using BlogApi.Models;
using BlogApi.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BlogApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PostController : ControllerBase
{
    private readonly BlogApiContext _context;
    private readonly IMapper _mapper;

    public PostController(BlogApiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Post
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsAsync()
    {
        var posts = await _context.Posts.ProjectTo<PostDto>(_mapper.ConfigurationProvider).ToListAsync();
        return Ok(posts);
    }

    // GET: api/Post/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<PostDto>> GetPostAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<PostDto>(post));
    }

    // PUT: api/Post/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPost(int id, PostDto postdto)
    {
        if (id != postdto.Id)
        {
            return BadRequest();
        }
        if (!PostExists(id))
        {
            return NotFound();
        }
        await _context.Posts.Persist(_mapper).InsertOrUpdateAsync<PostDto>(postdto);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/Post
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Post>> PostPostAsync(PostDto postdto)
    {
        await _context.Posts.Persist(_mapper).InsertOrUpdateAsync<PostDto>(postdto);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Post/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PostExists(int id)
    {
        return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}