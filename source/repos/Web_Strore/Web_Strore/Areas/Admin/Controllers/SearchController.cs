using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Web_Strore.Models;

namespace Web_Strore.Areas.Admin
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly STORE_WEBContext _context;

        public SearchController(STORE_WEBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult FindProduct(string keyword)
        {
            List<Product> ls = new List<Product>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListProductSearchPartial", null);
            }
            ls = _context.Products
                                .AsNoTracking()
                                .Include(a => a.Cat)
                                .Where(x => x.ProductName.Contains(keyword))
                                .OrderByDescending(x => x.ProductName)
                                .Take(10)
                                .ToList();
            if (ls == null)
            {
                return PartialView("ListProductSearchPartial", null);
            }
            else
            {
                return PartialView("ListProductSearchPartial", ls);
            }
        }


    }
}
