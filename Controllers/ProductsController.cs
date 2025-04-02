using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManager.Models;
using ProductManager.Models.DBContext;

namespace ProductManager.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductDBContext _context;
        private readonly CategoryDBContext _contextCategory;
        private readonly LocationDBContext _contextLocation;

        public ProductsController(ProductDBContext context, CategoryDBContext contextCategory, LocationDBContext contextLocation)
        {
            _context = context;
            _contextCategory = contextCategory;
            _contextLocation = contextLocation;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        { 
            var categories = await _contextCategory.Categories
                .Where(l => l.status)
                .OrderBy(c => c.description)
                .ToDictionaryAsync(c => c.idCategory, c => c.description);

            var locations = await _contextLocation.Locations
                .Where(l => l.status)
                .OrderBy(c => c.description)
                .ToDictionaryAsync(l => l.idLocation, l => l.code+"-"+l.description);

            ViewBag.Categories = categories;
            ViewBag.Locations = locations;

            return View(await _context.Products
                .Where(l => l.status)
                .OrderBy(c => c.description)
                .ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.idProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _contextCategory.Categories.Where(l => l.status).OrderBy(c => c.description).ToListAsync();
            ViewBag.Locations = await _contextLocation.Locations.Where(l => l.status).OrderBy(c => c.description).ToListAsync();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idProduct,description,price,stock,idCategory,idLocation,createDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.status = true;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _contextCategory.Categories.Where(l => l.status).OrderBy(c => c.description).ToListAsync();  
            ViewBag.Locations = await _contextLocation.Locations.Where(l => l.status).OrderBy(c => c.description).ToListAsync();
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idProduct,description,price,stock,idCategory,idLocation,createDate")] Product product)
        {
            if (id != product.idProduct)
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
                    if (!ProductExists(product.idProduct))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.idProduct == id);
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
            if (product == null)
            {
                return NotFound();
            }

            //Logic Delete
            product.status = false;

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.idCategory))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.idProduct == id);
        }
    }
}
