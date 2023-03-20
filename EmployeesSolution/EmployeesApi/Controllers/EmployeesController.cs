
namespace EmployeesApi.Controllers;

public class EmployeesController : ControllerBase
{
    //GET /employees

    [HttpGet("/employees")]
    public async Task<ActionResult<EmployeeSummaryResponse>> GetAllEmployees([FromQuery] string dept = "All")
    {
        var response = new EmployeeSummaryResponse(18, 10, 8, dept);
        return Ok(response);
    }

    [HttpGet("/employees/{employeeId}")]
    public async Task<ActionResult<EmployeeResponse>> GetEmployeeById([FromRoute] string employeeId)
    {
        if (int.Parse(employeeId) % 2 == 0)
        {
            var response = new EmployeeResponse(employeeId, "Bob", "Smith", "DEV");
            return Ok(response);
        }
        else
        {
            return NotFound();
        }
        // 200 Ok with that employee
        // 404
    }
}