using LimsEmployeService.Models;
using LimsEmployeService.Service;

public interface IEmployeService
{
    int CountEmployes();
    Task<List<Employe>> GetEmployesFrom(int skiped, int size);
    Task<Employe> GetEmploye(int id);
    Task<Employe> CreateEmploye(Employe employe);
    Task<Boolean> DeleteEmploye(int id);
    Task<Employe> EditEmploye(Employe employe);
}