using LimsEmployeService.Data;
using LimsEmployeService.Models;
using Microsoft.EntityFrameworkCore;

namespace LimsEmployeService.Service;

public class PosteService : IPosteService
{

    private readonly PosteContext _dbContext;
    public PosteService(PosteContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int CountPostes()
    {
        int result = _dbContext.Postes.Count();
        return result;
    }

    public async Task<Poste> CreatePoste(Poste poste)
    {
        _dbContext.Postes.Add(poste);
        await _dbContext.SaveChangesAsync();
        Poste result = await _dbContext.Postes.OrderBy(p => p.IdPoste).LastAsync();

        return result;
    }

    public async Task<bool> DeletePoste(int id)
    {
        bool isDeleted = false;
        Poste? poste = await _dbContext.Postes.FirstOrDefaultAsync(p => p.IdPoste== id);
        if(poste == null)
        {
            throw new ArgumentException("L'poste que vous souhaitez supprimer n'est pas dans la base de données");
        }
        _dbContext.Postes.Remove(poste);
        await _dbContext.SaveChangesAsync();
        isDeleted = true;
        return isDeleted;
    }

    public async Task<Poste> EditPoste(Poste poste)
    {
        int id = poste.IdPoste;
        _dbContext.Postes.Update(poste);
        await _dbContext.SaveChangesAsync();

        Poste result = await this.GetPoste(id);
        return result;
    }

    public async Task<Poste> GetPoste(int id)
    {
        Poste result = await _dbContext.Postes
            .Where(p => p.IdPoste == id)
            .FirstAsync();
        return result;
    }

    // Exclude DLAB and ADDLAB as this is used for Employe insertion
    public async Task<List<Poste>> GetPostes()
    {
        List<Poste> results = await _dbContext.Postes
        .Where(p => p.Designation != "DLAB" && p.Designation != "ADDLAB")
        .ToListAsync();
        return results;
    }

    public async Task<List<Poste>> GetPostesFrom(int skiped, int size)
    {
        List<Poste> results = await _dbContext.Postes.Skip(skiped).Take(size)
            .ToListAsync();

        return results;
    }
}