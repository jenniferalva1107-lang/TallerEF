using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerEF.Data;
using TallerEF.Models;

namespace TallerEF.Controllers
{
    public class ProductosController : Controller
    {
        private readonly TiendaContext _context;

        public ProductosController(TiendaContext context)
        {
            _context = context;
        }

        // READ - Listar
        public async Task<IActionResult> Index(string buscarNombre)
        {
            var productos = _context.Productos.AsQueryable();

            if (!string.IsNullOrEmpty(buscarNombre))
            {
                productos = productos.Where(p => p.Nombre.Contains(buscarNombre));
            }

            return View(await productos.ToListAsync());
        }

        // CREATE - Formulario
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "¡Producto registrado correctamente!";
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // UPDATE - Formulario
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.IdProducto) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(producto);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "¡Producto actualizado con éxito!";
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // DELETE - Confirmar
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var producto = await _context.Productos.FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "¡Producto eliminado con éxito!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}