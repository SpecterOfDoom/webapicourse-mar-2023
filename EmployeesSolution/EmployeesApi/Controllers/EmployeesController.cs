
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
			var contacts = new Dictionary<string, Dictionary<string, string>>() {
				{"home", new Dictionary<string, string> { {"email", "bob@aol.com" }, { "phone", "555-1212"} } },
				{"work", new Dictionary<string, string> { {"email", "bob@company.com"}, { "phone", "888-1212"} } }
			};
			var response = new EmployeeResponse(employeeId, new NameInformation("Bob", "Smith"), new WorkDetails("DEV"), contacts);
			return Ok(response);
		}
		else
		{
			return NotFound();
		}
	}
}