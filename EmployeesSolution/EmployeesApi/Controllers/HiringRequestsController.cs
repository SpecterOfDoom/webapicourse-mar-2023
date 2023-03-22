using Microsoft.EntityFrameworkCore;

namespace EmployeesApi.Controllers;
public class HiringRequestsController : ControllerBase
{
    
    private readonly EmployeesDataContext _context;
    
    public HiringRequestsController(EmployeesDataContext context)
    {
        _context = context;
    }

    public async Task<ActionResult> AddHiringRequest([FromBody] HiringRequestCreate request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var hiringEntity = new HiringRequestEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Note = request.Note ?? "",
            CreatedAt = DateTime.UtcNow,
            Status = HiringRequestStatus.PendingDepartment
        };
        _context.HiringRequests.Add(hiringEntity);
        await _context.SaveChangesAsync();
        // A post to a collection should return a 201 Created, a Link to the new item, and a copy of that item.
        return CreatedAtRoute("hiringrequests-get-by-id", new { id = hiringEntity.Id }, hiringEntity);
    }

    [HttpGet("/hiring-requests/{id:int}", Name = "hiringrequests-get-by-id")]
    public async Task<ActionResult> GetHiringRequest([FromRoute] int id)
    {
        var response = await _context.HiringRequests.SingleOrDefaultAsync(r => r.Id == id);
        return response is null ? NotFound() : Ok(response);
    }

}
