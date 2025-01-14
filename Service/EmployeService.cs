using LimsEmployeService.Data;
using LimsEmployeService.Models;
using Microsoft.EntityFrameworkCore;

namespace LimsEmployeService.Service;

public class EmployeService : IEmployeService
{
    private readonly PosteContext _dbContext;
    public EmployeService(PosteContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int CountEmployes()
    {
        int result = _dbContext.Employes.Count();
        return result;
    }

    public async Task<List<Employe>> GetEmployesFrom(int skiped, int size)
    {        
        List<Employe> results = await _dbContext.Employes.Skip(skiped).Take(size).ToListAsync();

        return results;
    }
}