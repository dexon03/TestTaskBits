using System.Diagnostics;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using TestTaskBits.Database;
using TestTaskBits.Extensions;
using TestTaskBits.Models;

namespace TestTaskBits.Controllers;

// [Route("{controller}/{action=Index}/{id?}")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var data = _context.Users.ToList();
        return View(data);
    }
    
    public IActionResult UploadCsv()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult UploadCsv(IFormCollection?  form)
    {
        if (form == null || !form.Files.Any() || form.Files[0].ContentType != "text/csv")
        {
            return Content("File is empty or wrong format");
        }
        using (var reader = new StreamReader(form.Files[0].OpenReadStream()))
        {
            // Read the CSV file content
            var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            try
            {
                var records = csvReader.GetRecords<User>().ToList();
                _context.Users.AddRange(records);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return Content("Data in csv in wrong format. Structure: Name, DateOfBirth, Phone, Salary, Married");
            }
        }
        
        return Redirect(nameof(Index));
    }
    
    public IActionResult Update(User user)
    {
        var userToUpdate = _context.Users.FirstOrDefault(x => x.Id == user.Id);
        if (userToUpdate != null)
        {
            userToUpdate.Map(user);
            
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Delete(Guid id)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}