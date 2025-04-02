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
    public class TransactionsController : Controller
    {
        private readonly TransactionDBContext _context;
        private readonly ProductDBContext _contextProduct;

        public TransactionsController(TransactionDBContext context, ProductDBContext contextProduct)
        {
            _context = context;
            _contextProduct = contextProduct;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var products = await _contextProduct.Products
               .OrderBy(c => c.description)
               .ToDictionaryAsync(p => p.idProduct, p => p.description);

            ViewBag.Products = products;

            return View(await _context.Transactions
                .OrderBy(c => c.idTransaction)
                .ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.idTransaction == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public async Task<IActionResult> Create() 
        { 
            ViewBag.Products = await _contextProduct.Products.Where(l => l.status).OrderBy(c => c.description).ToListAsync();
            ViewBag.TransactionTypes = Enum.GetValues(typeof(TypeTransaction));

            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idTransaction,idProduct,pieces,typeTransaction,comments,createdDate")] Transaction transaction)
        {
            ViewBag.Products = await _contextProduct.Products.Where(l => l.status).OrderBy(c => c.description).ToListAsync();
            ViewBag.TransactionTypes = Enum.GetValues(typeof(TypeTransaction));
            if (ModelState.IsValid)
            {
                try
                {
                    // Obtener el producto relacionado
                    var product = await _contextProduct.Products.FindAsync(transaction.idProduct);

                    if (product == null)
                    {
                        return NotFound();
                    }

                    // Actualizar el stock según el tipo de transacción
                    if (transaction.typeTransaction == TypeTransaction.Entrada)
                    {
                        product.stock += transaction.pieces;
                    }
                    else if (transaction.typeTransaction == TypeTransaction.Salida)
                    {
                        // Validar que haya suficiente stock
                        if (product.stock < transaction.pieces)
                        {
                            ModelState.AddModelError("", $"No hay suficiente stock. Stock actual: {product.stock}");
                            return View(transaction);
                        }
                        product.stock -= transaction.pieces;
                    }

                    _context.Add(transaction);
                    _contextProduct.Update(product);
                    await _context.SaveChangesAsync();
                    await _contextProduct.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Ocurrió un error al procesar la transacción: {ex.Message}");
                    return View(transaction);
                }
            }
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            ViewBag.Products = await _contextProduct.Products.Where(l => l.status).OrderBy(c => c.description).ToListAsync();
            ViewBag.TransactionTypes = Enum.GetValues(typeof(TypeTransaction));

            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idTransaction,idProduct,pieces,typeTransaction,comments,createdDate")] Transaction transaction)
        {
            if (id != transaction.idTransaction)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.idTransaction))
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
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.idTransaction == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.idTransaction == id);
        }
    }
}
