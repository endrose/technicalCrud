using System.Globalization;

namespace technicalTestCrud.Helpers
{
  public static class CurrencyHelper
  {
    public static string ToRupiah(decimal amount)
    {
      var culture = new CultureInfo("id-ID");
      return string.Format(culture, "Rp {0:N0}", amount);
    }
  }
}