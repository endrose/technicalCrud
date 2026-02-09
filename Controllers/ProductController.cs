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
        // Jika ada file yang diupload
        if (product.ImageFile != null && product.ImageFile.Length > 0)
        {
          // Buat folder jika belum ada
          var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
          if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

          // Nama file unik agar tidak bentrok
          var fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
          var filePath = Path.Combine(folderPath, fileName);

          // Simpan file
          using (var stream = new FileStream(filePath, FileMode.Create))
          {
            await product.ImageFile.CopyToAsync(stream);
          }

          // Simpan path/filename di DB
          product.ImagePath = "/images/" + fileName;
        }

        _context.Add(product);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Product Berhasil ditambahkan!";
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

    // =========================
    // UPDATE
    // =========================
    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
      if (id != product.Id)
        return NotFound();

      if (ModelState.IsValid)
      {
        var existingProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (existingProduct == null)
          return NotFound();

        // Jika ada file baru diupload
        if (product.ImageFile != null && product.ImageFile.Length > 0)
        {
          var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
          if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

          // Hapus file lama jika ada
          if (!string.IsNullOrEmpty(existingProduct.ImagePath))
          {
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingProduct.ImagePath.TrimStart('/'));
            if (System.IO.File.Exists(oldFilePath))
              System.IO.File.Delete(oldFilePath);
          }

          // Simpan file baru
          var fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
          var filePath = Path.Combine(folderPath, fileName);
          using (var stream = new FileStream(filePath, FileMode.Create))
          {
            await product.ImageFile.CopyToAsync(stream);
          }

          product.ImagePath = "/images/" + fileName;
        }
        else
        {
          // Tidak upload file baru, tetap pakai file lama
          product.ImagePath = existingProduct.ImagePath;
        }

        try
        {
          _context.Update(product);
          await _context.SaveChangesAsync();
          TempData["SuccessMessage"] = "Product Berhasil diubah!";
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!_context.Products.Any(e => e.Id == id))
            return NotFound();
          else
            throw;
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

    // DELETE
    // =========================
    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var product = await _context.Products.FindAsync(id);
      if (product != null)
      {
        // Hapus file gambar jika ada
        if (!string.IsNullOrEmpty(product.ImagePath))
        {
          var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImagePath.TrimStart('/'));
          if (System.IO.File.Exists(filePath))
            System.IO.File.Delete(filePath);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Product Berhasil dihapus!";
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