using EmployeesApi.Adapaters;
using EmployeesApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EmployeesApi.Domain;

public class DepartmentsLookup
{
    private readonly EmployeesDataContext _context;

    public DepartmentsLookup(EmployeesDataContext context) 
    {
        _context = context;
    }
    public async Task<List<DepartmentItem>> GetDepartmentsAsync()
    {
        var response = await _context.Departments
            .Select(dept => new DepartmentItem(dept.Code, dept.Description))
            .ToListAsync();
        return response;
    }
}
