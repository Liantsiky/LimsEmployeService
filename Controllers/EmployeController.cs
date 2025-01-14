using LimsEmployeService.Models;
using LimsEmployeService.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LimsEmployeService.Controllers;
[ApiController]
[Route("api/employe")]
public class EmployeController : ControllerBase
{
    private readonly IEmployeService _employeService;
    public EmployeController(IEmployeService employeeservice)
    {
        _employeService = employeeservice;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetEmployes(int position, int pageSize)
    {
        if (position == 0) position = 1;
        if (pageSize == 0) pageSize = 2;
        Dictionary<string, object> response = new Dictionary<string, object>();
        response["nbrPerPage"] = pageSize;
        response["TotalCount"] = _employeService.CountEmployes();
        response["nbrLinks"] = Math.Ceiling((double)_employeService.CountEmployes() / pageSize);

            response["position"] = position;
            int skiped = (position-1) * pageSize;
            List<Employe> employes = await _employeService.GetEmployesFrom(skiped, pageSize);
            return Ok(new ApiResponse
            {
                Data = employes,
                ViewBag = response,
                IsSuccess = true,
                Message = "Datas retrieved successfully.",
                StatusCode = 200
            });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> GetEmploye(int id)
    {
        Employe employe = await _employeService.GetEmploye(id);
        if(employe == null) return NotFound();

        return Ok(new ApiResponse
        {
            Data = employe,
            ViewBag = null,
            IsSuccess = true,
            Message = "Data retrieved successfully",
            StatusCode = 200
        });
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateEmploye(Employe employe)
    {
        Dictionary<string, object> response = new Dictionary<string, object>();
        Employe createdEmploye = await _employeService.CreateEmploye(employe);
        return CreatedAtAction(nameof(GetEmploye), new { id = createdEmploye}, new ApiResponse
        {
            Data = employe,
            ViewBag = null,
            IsSuccess = true,
            Message = "Created successfully",
            StatusCode = 201
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmploye(int id)
    {
        await _employeService.DeleteEmploye(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse>> UpdateEmploye(int? id, Employe employe)
    {
        if(id == null) return NotFound();
        Employe updatedEmploye =await _employeService.EditEmploye(employe);
        return CreatedAtAction(nameof(GetEmploye), new { id = updatedEmploye.IdEmploye }, new ApiResponse
        {
            Data = updatedEmploye,
            ViewBag = null,
            IsSuccess = true,
            Message = "Created successfully",
            StatusCode = 201
        });
    }
}