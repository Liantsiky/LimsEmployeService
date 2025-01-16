using Microsoft.AspNetCore.Mvc;
using LimsEmployeService.Models;
using Microsoft.EntityFrameworkCore;

using LimsEmployeService.Utils;
using LimsEmployeService.Service;

namespace LimsEmployeService.Controllers;

[ApiController]
[Route("api/poste")]
public class PosteController : ControllerBase
{
    private readonly IPosteService _posteService;

    public PosteController(IPosteService posteService)
    {
        _posteService = posteService;
    }

    [HttpGet]
    [Route("/api/poste/all")]
    public async Task<ActionResult<ApiResponse>> GetAllPostes()
    {
        List<Poste> postes = await _posteService.GetPostes();
        return Ok(new ApiResponse
        {
            Data = postes,
            ViewBag = null,
            IsSuccess = true,
            Message = "Datas retrieved successfully.",
            StatusCode = 200
        });
    }

    [HttpGet]
    public async Task<ActionResult> GetPoste(int position, int pageSize)
    {
        if (position == 0) position = 1;
        if (pageSize == 0) pageSize = 2;
        Dictionary<string, object> response = new Dictionary<string, object>();
        response["nbrPerPage"] = pageSize;
        response["TotalCount"] = _posteService.CountPostes();
        response["nbrLinks"] = Math.Ceiling((double)_posteService.CountPostes() / pageSize);

            response["position"] = position;
            int skiped = (position-1) * pageSize;
            List<Poste> postes = await _posteService.GetPostesFrom(skiped, pageSize);
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
        Poste poste = await _posteService.GetPoste(id);
        if(poste == null) return NotFound();

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
        Dictionary<string, object> response = new Dictionary<string, object>();
        Poste createdEmploye = await _posteService.CreatePoste(poste);
        return CreatedAtAction(nameof(GetPoste), new { id = createdEmploye}, new ApiResponse
        {
            Data = poste,
            ViewBag = null,
            IsSuccess = true,
            Message = "Created successfully",
            StatusCode = 201
        });
    }

    // PUT: api/poste/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePoste(int? id, Poste poste)
    {
        if(id == null) return NotFound();
        Poste updatedPoste =await _posteService.EditPoste(poste);
        return CreatedAtAction(nameof(GetPoste), new { id = updatedPoste.IdPoste }, new ApiResponse
        {
            Data = updatedPoste,
            ViewBag = null,
            IsSuccess = true,
            Message = "Created successfully",
            StatusCode = 201
        });
    }

    // DELETE: api/poste/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePoste(int id)
    {
        await _posteService.DeletePoste(id);
        return NoContent();
    }
}
