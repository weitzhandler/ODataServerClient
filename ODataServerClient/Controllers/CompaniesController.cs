using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Server.Data;

namespace Server.Controllers
{

  public class CompaniesController : ODataController
  {
    readonly AppDbContext _DbContext;
    readonly ILogger _Logger;
    public CompaniesController(AppDbContext dbContext, ILogger<CompaniesController> logger)
    {
      _DbContext = dbContext;
      _Logger = logger;
    }

    [EnableQuery]
    public ActionResult<IQueryable<Company>> Get()
    {
      _Logger.LogInformation("Companies requested.");

      return _DbContext.Companies;
    }

    [EnableQuery]
    public async Task<ActionResult<Company>> Get(int key)
    {
      _Logger.LogInformation($"Company by ID requested with key{key}.");
      var result = await _DbContext.Companies.FirstOrDefaultAsync(c => c.Id == key);

      if (result == null)
      {
        _Logger.LogError($"Company by ID requested with key{key} not found.");
        return NotFound(key);
      }

      _Logger.LogInformation($"Company by ID requested with key{key} found.");
      return result;
    }

    public async Task<IActionResult> Post([FromBody] Company company)
    {
      if (company == null || !ModelState.IsValid || company.Id > 0)
      {
        _Logger.LogError("Invalid company");
        return BadRequest(ModelState);
      }

      _DbContext.Add(company);
      try
      {
        await _DbContext.SaveChangesAsync();
      }
      catch (Exception e)
      {
        _Logger.LogError(e, "error creating company.");
        return base.StatusCode(StatusCodes.Status500InternalServerError, company);
      }

      return Created(company);
    }


  }
}
