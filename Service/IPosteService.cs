using LimsEmployeService.Models;

namespace LimsEmployeService.Service;

public interface IPosteService
{
    Task<List<Poste>> GetPostes();
    Task<Poste> GetPoste(int id);
    Task<Poste> CreatePoste(Poste poste);
    Task<bool> DeletePoste(int id);
    Task<Poste> EditPoste(Poste poste);
    int CountPoste();
    Task<List<Poste>> GetPostesFrom(int skiped, int size);
}