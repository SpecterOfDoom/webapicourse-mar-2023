

using EmployeesApi.Controllers.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApi.Controllers;

public class EmployeesController : ControllerBase
{
    private readonly EmployeesDataContext _context;
    private readonly ILookupEmployees _employeeLookupService;
    private readonly IManageEmployees _employeeManager;

    public EmployeesController(EmployeesDataContext context, ILookupEmployees employeeLookupService,IManageEmployees employeeManager)
    {
        _context = context;
        _employeeLookupService = employeeLookupService;
        _employeeManager = employeeManager;
    }

    [HttpPut("/employees/{employeeId}/contact-information/home")]
    public async Task<ActionResult> UpdateHomeContactInformation([FromRoute] string employeeId, [FromBody] HomeContactItem contactItem)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var foundAndUpdated = await _employeeManager.UpdateContactInfoAsync(employeeId, contactItem);
        return foundAndUpdated ? Ok(contactItem) : NotFound();
    }



    [HttpGet("/employees/{employeeId}/contact-information/home")]
    public async Task<ActionResult<ContactItem>> GetEmployeeHomeContactInfo(string employeeId)
    {
        ContactItem? response = await _employeeLookupService.GetEmployeeContactInfoForHomeAsync(employeeId);
        
        return response is null ? NotFound() : Ok(response);
    }

    [HttpGet("/employees/{employeeId}/contact-information/work")]
    public async Task<ActionResult<ContactItem>> GetEmployeeWorkContactInfo(string employeeId)
    {
        ContactItem? response = await _employeeLookupService.GetEmployeeContactInfoForWorkAsync(employeeId);

        return response is null ? NotFound() : Ok(response);
    }

    // GET /employees
    // make URIs CASE SENSITIVE - always do them the same way.
    [HttpGet("/employees")]
    public async Task<ActionResult<EmployeeSummaryResponse>> GetAllEmployees([FromQuery] string dept = "All")
    {
        var response = new EmployeeSummaryResponse(18, 10, 8, dept);
        return Ok(response); // 200 Ok, but serialize this .NET object to client.

    }

    [HttpGet("/employees/{employeeId}")]
    public async Task<ActionResult<EmployeeResponse>> GetEmployeeById([FromRoute] string employeeId)
    {
        EmployeeResponse? response =await  _employeeLookupService.GetEmployeeByIdAsync(employeeId);
        
        if(response is null)
        {
            return NotFound();
        } else
        {
            return Ok(response);
        }

    }
}
