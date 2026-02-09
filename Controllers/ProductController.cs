using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using technicalTestCrud.Data;
using technicalTestCrud.Models;

namespace technicalTestCrud.Controllers
{
  public class ProductsController : Controller
  {
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
      _context = context;
    }

    // =========================
    // READ - List
    // =========================
    // GET: Products
    public async Task<IActionResult> Index()
    {
      var products = await _context.Products.ToListAsync();
      return View(products);
    }

    // =========================
    // CREATE
    // =========================
    // GET: Products/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
      if (ModelState.IsValid)
      {
        _context.Add(product);
        await _context.SaveChangesAsync();
        //redurect ke Controller Home dan View Index
        return RedirectToAction("Index", "Home");
      }
      return View(product);
    }

    // =========================
    // UPDATE
    // =========================
    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
      var product = await _context.Products.FindAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      return View(product);
    }

    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
      if (id != product.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(product);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!_context.Products.Any(e => e.Id == id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction("Index", "Home");
      }
      return View(product);
    }

    // =========================
    // DELETE
    // =========================
    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
      var product = await _context.Products
        .FirstOrDefaultAsync(m => m.Id == id);

      if (product == null)
      {
        return NotFound();
      }

      return View(product);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var product = await _context.Products.FindAsync(id);
      if (product != null)
      {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
      }

      return RedirectToAction("Index", "Home");
    }

    // =========================
    // DETAILS
    // =========================
    // GET: Products/Details/5
    public async Task<IActionResult> Details(int id)
    {
      var product = await _context.Products
        .FirstOrDefaultAsync(m => m.Id == id);

      if (product == null)
      {
        return NotFound();
      }

      return View(product);
    }
  }

  
}