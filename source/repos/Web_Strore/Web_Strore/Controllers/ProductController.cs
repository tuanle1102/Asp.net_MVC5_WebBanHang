using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Strore.Models;
using X.PagedList;

namespace Web_Strore.Controllers
{
    public class ProductController : Controller
    {
        private readonly STORE_WEBContext _context;
        public INotyfService _notyfService { get; }

        public ProductController(STORE_WEBContext context) {
            _context = context;
        }
        [Route("shop.html", Name = "ShopProduct")]
        public IActionResult Index(int? page, int CatID = 0)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 13;
                var lsProducts = _context.Products
                    .AsNoTracking()
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName", CatID);

                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Route("danhmuc/{Alias}", Name = "ListProduct")]
        public IActionResult List(string Alias, int page = 1)
        {
            try
            {
                var pageSize = 13;
                var danhmuc = _context.Categories.AsNoTracking().SingleOrDefault(x=>x.Alias==Alias);
                var lsProducts = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CatId == danhmuc.CatId)
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsProducts, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                return View(models);
            }
            catch {
                return RedirectToAction("Index", "Home");
            }
        }


        public IActionResult Filtter(int CatID = 0)
        {
            //var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var url = $"/Views/Product?CatID={CatID}";
            if (CatID == 0)
            {
                url = $"/Views/Product";
            }
            else
            {
                if (CatID == 0) url = $"/Views/Product?CatID={CatID}";
            }
            return Json(new { status = "success", redirectUrl = url });

        }


        [Route("/{Alias}-{id}.html",Name = "ProductDetails")] 
        public IActionResult Details(int id) {
            try
            {
                var product = _context.Products.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                var lsProducts = _context.Products.AsNoTracking()
                    .Where(x => x.CatId == product.CatId && x.ProductId != id && x.Active == true)
                    .OrderByDescending(x => x.DateCreated)
                    .Take(6)
                    .ToList();

                ViewBag.SanPham = lsProducts;
                return View(product);
            }
            catch {
                return RedirectToAction("Index", "Home");
            }

        }

    }
}
