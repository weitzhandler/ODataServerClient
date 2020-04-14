using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Controllers
{

  public class CompaniesController : Controller
  {
    readonly AppDbContext _DbContext;
    public CompaniesController(AppDbContext dbContext)
    {
      _DbContext = dbContext;
    }

    [EnableQuery]
    public IEnumerable<Company> Get()
    {
      var x = _DbContext.Companies.ToArray();

      return _DbContext.Companies;
    }
  }
}
