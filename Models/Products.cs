using System.ComponentModel.DataAnnotations.Schema;

namespace technicalTestCrud.Models
{
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }
    // Path / nama file gambar
    public string? ImagePath { get; set; }

    // Tidak disimpan ke DB, hanya untuk upload
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
  }
}