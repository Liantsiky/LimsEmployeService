using System.Text.Json;
using LimsEmployeService.Data;
using LimsEmployeService.Dtos;
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

    public async Task<int> CountEmployes()
    {
        int result = await _dbContext.Employes.CountAsync();
        return result;
    }

    public async Task<List<Employe>> GetEmployesFrom(int skiped, int size)
    {        
        List<Employe> results = await _dbContext.Employes
            .Where(e => e.Statut == 0)
            .OrderByDescending(e => e.IdEmploye).Skip(skiped).Take(size)
            .Include(employe => employe.Poste)
            .Include(employe => employe.Departement)
            .ToListAsync();

        return results;
    }

    public async Task<Employe> GetEmploye(int id)
    {
        Employe result = await _dbContext.Employes.
            Include(employe => employe.Poste)
            .Include(employe => employe.Departement)
            .Include(employe => employe.HistoriqueEmployes)
            .Where(e => e.IdEmploye == id)
            .FirstAsync();
        return result;
    }

    public async Task<Employe> CreateEmploye(EmployeDto employe)
    {
        Employe result = new Employe();

        result = await result.HandleDtosForInsert(employe);

        _dbContext.Employes.Add(result);
        await _dbContext.SaveChangesAsync();
        result = await GetEmploye(employe.IdEmploye);

        return result;
    }

    public async Task<bool> DeleteEmploye(int id, EmployeDto employe)
    {
        bool result = false;
        try{
            Employe emp = new Employe();
            Employe empToUpdate = await emp.HandleDtoForDelete(employe, _dbContext);  
            Console.WriteLine(JsonSerializer.Serialize(empToUpdate)); 

            _dbContext.Employes.Update(empToUpdate);
            await _dbContext.SaveChangesAsync();
        }catch (Exception)
        {
            throw;
        }

        result = true;
        return result;
    }

    public async Task<Employe> EditEmploye(EmployeDto employe)
    {
        int id = employe.IdEmploye;
        Employe result = new Employe();
        Employe empToUpdate = await result.HandleDtoForUpdate(employe, _dbContext);   

        _dbContext.Employes.Update(empToUpdate);
        await _dbContext.SaveChangesAsync();

        result = await GetEmploye(id);
        return result;
    }
}