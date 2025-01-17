namespace LimsEmployeService.Service;

using LimsEmployeService.Models;

public interface IEmployeService
{
    Task<int> CountEmployes();
    Task<List<Employe>> GetEmployesFrom(int skiped, int size);
    Task<Employe> GetEmploye(int id);
    Task<Employe> CreateEmploye(Employe employe);
    Task<bool> DeleteEmploye(int id);
    Task<Employe> EditEmploye(Employe employe);
}