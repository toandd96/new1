using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    public class TinnhanhsController : Controller
    {
        private readonly WebTTContext _context;

        public TinnhanhsController(WebTTContext context)
        {
            _context = context;    
        }

        // GET: Tinnhanhs
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var webTTContext = _context.Tinnhanh.Include(t => t.MachuyenmucNavigation);
            return View(await webTTContext.ToListAsync());
        }

        // GET: Tinnhanhs/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinnhanh = await _context.Tinnhanh
                .Include(t => t.MachuyenmucNavigation)
                .SingleOrDefaultAsync(m => m.Matinnhanh == id);
            if (tinnhanh == null)
            {
                return NotFound();
            }

            return View(tinnhanh);
        }

        // GET: Tinnhanhs/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["Machuyenmuc"] = new SelectList(_context.Chuyenmuc, "Machuyenmuc", "Tenchuyenmuc");
            return View();
        }

        // POST: Tinnhanhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Matinnhanh,Noidung,Machuyenmuc")] Tinnhanh tinnhanh,Chuyenmuc chuyenmuc)
        {
            if (ModelState.IsValid)
            {
                var sbv =await _context.Chuyenmuc.SingleOrDefaultAsync(c => c.Machuyenmuc == tinnhanh.Machuyenmuc);
                sbv.Sobaiviet = sbv.Sobaiviet + 1;
                _context.Add(tinnhanh);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Machuyenmuc"] = new SelectList(_context.Chuyenmuc, "Machuyenmuc", "Tenchuyenmuc", tinnhanh.Machuyenmuc);
            return View(tinnhanh);
        }

        // GET: Tinnhanhs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinnhanh = await _context.Tinnhanh.SingleOrDefaultAsync(m => m.Matinnhanh == id);
            if (tinnhanh == null)
            {
                return NotFound();
            }
            ViewData["Machuyenmuc"] = new SelectList(_context.Chuyenmuc, "Machuyenmuc", "Tenchuyenmuc", tinnhanh.Machuyenmuc);
            return View(tinnhanh);
        }

        // POST: Tinnhanhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Matinnhanh,Noidung,Machuyenmuc")] Tinnhanh tinnhanh)
        {
            if (id != tinnhanh.Matinnhanh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //tạo đối tượng Tinnhanh
                    Tinnhanh tinnhanhchuasua =await _context.Tinnhanh.AsNoTracking().Where(tn => tn.Matinnhanh == tinnhanh.Matinnhanh).SingleOrDefaultAsync();
                    //chuyên mục bị sửa
                    Chuyenmuc chuyenmucbisua = await _context.Chuyenmuc.SingleOrDefaultAsync(cm => cm.Machuyenmuc == tinnhanhchuasua.Machuyenmuc);
                    chuyenmucbisua.Sobaiviet = chuyenmucbisua.Sobaiviet - 1;
                    //chuyên mục sau khi sửa
                    Chuyenmuc chuyenmuc = await _context.Chuyenmuc.SingleOrDefaultAsync(cm => cm.Machuyenmuc == tinnhanh.Machuyenmuc);
                    chuyenmuc.Sobaiviet += chuyenmuc.Sobaiviet;
                    _context.Update(tinnhanh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinnhanhExists(tinnhanh.Matinnhanh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["Machuyenmuc"] = new SelectList(_context.Chuyenmuc, "Machuyenmuc", "Tenchuyenmuc", tinnhanh.Machuyenmuc);
            return View(tinnhanh);
        }

        // GET: Tinnhanhs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinnhanh = await _context.Tinnhanh
                .Include(t => t.MachuyenmucNavigation)
                .SingleOrDefaultAsync(m => m.Matinnhanh == id);
            if (tinnhanh == null)
            {
                return NotFound();
            }

            return View(tinnhanh);
        }

        // POST: Tinnhanhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id,Chuyenmuc cm)
        {

            var tinnhanh = await _context.Tinnhanh.SingleOrDefaultAsync(am => am.Matinnhanh == id);
            var chuyenmuc =await _context.Chuyenmuc.SingleOrDefaultAsync(c => c.Machuyenmuc == tinnhanh.Machuyenmuc);
            chuyenmuc.Sobaiviet = chuyenmuc.Sobaiviet - 1;
            _context.Tinnhanh.Remove(tinnhanh);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TinnhanhExists(int id)
        {
            return _context.Tinnhanh.Any(e => e.Matinnhanh == id);
        }
    }
}
