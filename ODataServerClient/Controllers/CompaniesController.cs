using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

      var result = await _DbContext.Companies.FindAsync(key);

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
        return base.StatusCode(StatusCodes.Status500InternalServerError, e);
      }

      return Created(company);
    }

    public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Company> company)
    {
      if (company == null || !ModelState.IsValid)
      {
        _Logger.LogError("Invalid company");
        return BadRequest(ModelState);
      }

      var storageCompany = await _DbContext.Companies.FindAsync(key);
      if (storageCompany == null)
      {
        return NotFound();
      }

      company.Patch(storageCompany);
      try
      {
        await _DbContext.SaveChangesAsync();
      }
      catch (Exception e)
      {
        _Logger.LogError(e, "error creating company.");
        return base.StatusCode(StatusCodes.Status500InternalServerError, e);
      }

      return Updated(storageCompany);
    }

    public async Task<IActionResult> Put([FromBody] Company company)
    {
      if (company == null || !ModelState.IsValid || company.Id == 0)
      {
        _Logger.LogError("Invalid company");
        return BadRequest(ModelState);
      }

      _DbContext.Companies.Update(company);
      try
      {
        await _DbContext.SaveChangesAsync();
      }
      catch (Exception e)
      {
        _Logger.LogError(e, "error creating company.");
        return base.StatusCode(StatusCodes.Status500InternalServerError, e);
      }

      return Updated(company);
    }

    public async Task<IActionResult> Delete([FromODataUri]int key)
    {
      if (key == 0)
      {
        _Logger.LogError("Invalid company id");
        return NotFound();
      }

      var entity = await _DbContext.Companies.FindAsync(key);
      if (entity == null)
      {
        _Logger.LogError("Invalid company id");
        return NotFound();
      }

      _DbContext.Companies.Remove(entity);
      try
      {
        await _DbContext.SaveChangesAsync();
      }
      catch (Exception e)
      {
        _Logger.LogError(e, "error creating company.");
        return base.StatusCode(StatusCodes.Status500InternalServerError, e);
      }

      return StatusCode(StatusCodes.Status204NoContent);
    }




  }
}
