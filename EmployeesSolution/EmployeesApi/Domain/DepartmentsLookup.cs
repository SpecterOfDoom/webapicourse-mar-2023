using EmployeesApi.Adapaters;
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
                return new List<DepartmentItem>
        {
            new DepartmentItem("DEV", "Developers"),
            new DepartmentItem("QA", "Quality Assurance Analysts"),
            new DepartmentItem("SALES", "Sales Engineers")
        };

    }
}
