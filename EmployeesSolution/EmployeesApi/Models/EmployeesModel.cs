namespace EmployeesApi.Models;

public class EmployeesModel
{
    
}

public record EmployeeSummaryResponse(int Total, int FullTime, int PartTime, string DepartmentFilter);

public record EmployeeResponse(string Id, string FirstName, string LastName, string Department);