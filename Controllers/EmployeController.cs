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
    // public async Task<IActionResult> GetEmployes(int position, int pageSize)
    public IActionResult GetEmployes(int position, int pageSize)
    {
        if (position == 0) position = 1;
        if (pageSize == 0) pageSize = 2;
        Dictionary<string, object> response = new Dictionary<string, object>();
        response["nbrPerPage"] = pageSize;
        response["TotalCount"] = _employeService.CountEmployes();
        response["nbrLinks"] = Math.Ceiling((double)_employeService.CountEmployes() / pageSize);

            response["position"] = position;
            int skiped = ((int)response["position"]-1) * pageSize;
            // List<Employe> employes = await _employeService.GetEmployesFrom(skiped, pageSize);
            // return Ok(new ApiResponse
            // {
            //     Data = employes,
            //     ViewBag = response,
            //     IsSuccess = true,
            //     Message = "Datas retrieved successfully.",
            //     StatusCode = 200
            // });
            return Ok(skiped);
    } 
}