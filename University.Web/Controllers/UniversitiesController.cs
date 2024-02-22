using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Entity.Entities;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
namespace Controllers
{
  public class UniversitiesController : Controller
  {
    private readonly UniversityDbContext _context;

    public UniversitiesController(UniversityDbContext context)
    {
      _context = context;
    }

    public IActionResult Index()
    {
      ViewBag.universities = _context.Universities.ToList();
      return View();
    }
    [HttpGet]
    public IActionResult New()
    {
      ViewBag.university = new Unversity();
      return View();
    }

    [HttpPost]
    public IActionResult Create(Unversity universityModel)
    {
      _context.Universities.Add(universityModel);
      _context.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {

      var university = _context.Universities.Find(id);
      if (university == null)
      {
        return NotFound();
      }
      _context.Remove(university);
      _context.SaveChanges();
      return Ok();

    }
  }
}