using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    public class ChuyenmucsController : Controller
    {
        private readonly WebTTContext _context;

        public ChuyenmucsController(WebTTContext context)
        {
            _context = context;    
        }
        [Authorize]
        // GET: Chuyenmucs1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chuyenmuc.ToListAsync());
        }

        // GET: Chuyenmucs1/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuyenmuc = await _context.Chuyenmuc
                .SingleOrDefaultAsync(m => m.Machuyenmuc == id);
            if (chuyenmuc == null)
            {
                return NotFound();
            }

            return View(chuyenmuc);
        }

        // GET: Chuyenmucs1/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chuyenmucs1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Machuyenmuc,Tenchuyenmuc,Sobaiviet")] Chuyenmuc chuyenmuc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chuyenmuc);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(chuyenmuc);
        }

        // GET: Chuyenmucs1/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuyenmuc = await _context.Chuyenmuc.SingleOrDefaultAsync(m => m.Machuyenmuc == id);
            if (chuyenmuc == null)
            {
                return NotFound();
            }
            return View(chuyenmuc);
        }

        // POST: Chuyenmucs1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Machuyenmuc,Tenchuyenmuc,Sobaiviet")] Chuyenmuc chuyenmuc)
        {
            if (id != chuyenmuc.Machuyenmuc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chuyenmuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChuyenmucExists(chuyenmuc.Machuyenmuc))
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
            return View(chuyenmuc);
        }

        // GET: Chuyenmucs1/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuyenmuc = await _context.Chuyenmuc
                .SingleOrDefaultAsync(m => m.Machuyenmuc == id);
            if (chuyenmuc == null)
            {
                return NotFound();
            }

            return View(chuyenmuc);
        }

        // POST: Chuyenmucs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chuyenmuc = await _context.Chuyenmuc.SingleOrDefaultAsync(m => m.Machuyenmuc == id);
            _context.Chuyenmuc.Remove(chuyenmuc);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ChuyenmucExists(int id)
        {
            return _context.Chuyenmuc.Any(e => e.Machuyenmuc == id);
        }
    }
}
