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
        List<Employe> results = await _dbContext.Employes.OrderByDescending(e => e.IdEmploye).Skip(skiped).Take(size).
            Include(employe => employe.Poste).
            Include(employe => employe.Departement)
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

    public async Task<bool> DeleteEmploye(int id)
    {
        bool isDeleted = false;
        Employe? employe = await _dbContext.Employes.FirstOrDefaultAsync(e => e.IdEmploye == id);
        if(employe == null)
        {
            throw new ArgumentException("L'employe que vous souhaitez supprimer n'est pas dans la base de données");
        }
        _dbContext.Employes.Remove(employe);
        await _dbContext.SaveChangesAsync();
        isDeleted = true;
        return isDeleted;
    }

    public async Task<Employe> EditEmploye(Employe employe)
    {
        int id = employe.IdEmploye;
        _dbContext.Employes.Update(employe);
        await _dbContext.SaveChangesAsync();

        Employe result = await GetEmploye(id);
        return result;
    }
}