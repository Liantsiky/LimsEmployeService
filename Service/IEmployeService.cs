using LimsEmployeService.Models;
using LimsEmployeService.Service;

public interface IEmployeService
{
    int CountEmployes();
    Task<List<Employe>> GetEmployesFrom(int skiped, int size);
}