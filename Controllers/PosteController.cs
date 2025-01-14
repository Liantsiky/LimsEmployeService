using Microsoft.AspNetCore.Mvc;
using LimsEmployeService.Data;
using LimsEmployeService.Models;
using Microsoft.EntityFrameworkCore;

using LimsEmployeService.Utils;

namespace LimsEmployeService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PosteController : ControllerBase
{
    private readonly PosteContext _context;

    public PosteController(PosteContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetPoste(int position, int pageSize)
    {
        if (position == 0) position = 1;
        if (pageSize == 0) pageSize = 2;
        Dictionary<string, object> response = new Dictionary<string, object>();
        int nbrPerPage = pageSize;
        response["nbrPerPage"] = nbrPerPage;
        response["TotalCount"] = _context.Postes.Count();
        response["nbrLinks"] = Math.Ceiling((double)_context.Postes.Count() / nbrPerPage);

            response["position"] = position;
            List<Poste> postes = await _context.Postes.Skip(((int)response["position"]-1) * nbrPerPage).Take(nbrPerPage).ToListAsync();
            return Ok(new ApiResponse
            {
                Data = postes,
                ViewBag = response,
                IsSuccess = true,
                Message = "Datas retrieved successfully.",
                StatusCode = 200
            });
    }
    // GET: api/poste/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Poste>> GetPosteDetails(int id)
    {
        var poste = await _context.Postes.FindAsync(id);

        if (poste == null)
        {
            return NotFound();
        }

        return Ok(new ApiResponse
        {
            Data = poste,
            ViewBag = null,
            IsSuccess = true,
            Message = "Data retrieved successfully",
            StatusCode = 200
        });
    }

    // POST: api/poste
    [HttpPost]
    public async Task<ActionResult<Poste>> CreatePoste(Poste poste)
    {
        _context.Postes.Add(poste);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPoste), new { id = poste.IdPoste }, new ApiResponse 
        {
            Data = poste,
            ViewBag = null,
            IsSuccess = true,
            Message = "Data created successfully",
            StatusCode = 201
        });
    }

    // PUT: api/poste/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePoste(int id, Poste poste)
    {
        if (id != poste.IdPoste)
        {
            return BadRequest();
        }

        _context.Entry(poste).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Postes.Any(e => e.IdPoste == id))
            {
                return NotFound();
            }
            throw;
        }

        return CreatedAtAction(nameof(GetPoste), new { id = poste.IdPoste }, new ApiResponse 
        {
            Data = await _context.Postes.FirstOrDefaultAsync(p => p.IdPoste == poste.IdPoste),
            ViewBag = null,
            IsSuccess = true,
            Message = "Data created successfully",
            StatusCode = 201
        });;
    }

    // DELETE: api/poste/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePoste(int id)
    {
        var poste = await _context.Postes.FindAsync(id);
        if (poste == null)
        {
            return NotFound();
        }

        _context.Postes.Remove(poste);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
