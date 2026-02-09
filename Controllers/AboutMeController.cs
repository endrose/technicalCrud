using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using technicalTestCrud.Data;
using technicalTestCrud.Models;

namespace technicalTestCrud.Controllers;

public class AboutMeController : Controller
{
    private readonly ILogger<AboutMeController> _logger;
    private readonly AppDbContext _context;

    public AboutMeController(ILogger<AboutMeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
}