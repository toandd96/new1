using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    public class TintucsController : Controller
    {

        private readonly WebTTContext _context;
        private readonly IHostingEnvironment _enviroment;


        public TintucsController(WebTTContext context, IHostingEnvironment enviroment)
        {
            _enviroment = enviroment;
            _context = context;

        }
        public JsonResult getalltintuc()
        {
            var tintuc = _context.Tintuc.OrderByDescending(p => p.Ngaydang);
            return Json(from tt in tintuc
                        select new
                        {
                            matintuc = tt.Matintuc,
                            tieude = tt.Tieude,
                            tieudecon = tt.Tieudecon,
                            noidung = tt.Noidung,
                            anh = tt.Anh,
                            ngaydang = tt.Ngaydang,
                            tacgia = tt.Tacgia,
                            machuyenmuc = tt.Machuyenmuc
                        });
        }
        // GET: Tintucs
        [HttpGet]
        [Authorize]
        //[ValidateInput(false)]
        public async Task<IActionResult> Index()
        {
            var webTTContext = _context.Tintuc.Include(t => t.MachuyenmucNavigation);
            return View(await webTTContext.ToListAsync());
        }

        // GET: Tintucs/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintuc
                .Include(t => t.MachuyenmucNavigation)
                .SingleOrDefaultAsync(m => m.Matintuc == id);
            if (tintuc == null)
            {
                return NotFound();
            }

            return View(tintuc);
        }

        // GET: Tintucs/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["Machuyenmuc"] = new SelectList(_context.Chuyenmuc, "Machuyenmuc", "Tenchuyenmuc");
            return View();
        }

        // POST: Tintucs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matintuc,Tieude,Tieudecon,Noidung,Anh,Ngaydang,Tacgia,Machuyenmuc")] Tintuc tintuc, IFormFile Anh, Chuyenmuc chuyenmuc)
        {
            if (ModelState.IsValid)
            {
                if (Anh.Length > 0)
                {
                    var uploadpath = Path.Combine(_enviroment.WebRootPath, "images");
                    Directory.CreateDirectory(Path.Combine(uploadpath));
                    string filename = Anh.FileName;
                    if (filename.Contains('\\'))
                    {
                        filename = filename.Split('\\').Last();
                    }
                    using (FileStream fileStream = new FileStream(Path.Combine(uploadpath, filename), FileMode.Create))
                    {
                        await Anh.CopyToAsync(fileStream);

                        tintuc.Anh = Anh.FileName;
                    }
                }
                tintuc.Ngaydang = DateTime.Now;
                var sbv = _context.Chuyenmuc.Where(c => c.Machuyenmuc == tintuc.Machuyenmuc);
                var sbd = sbv.Select(c => c.Sobaiviet);
                var tbv = sbv.Select(c => c.Tenchuyenmuc);
                var t = string.Empty;
                var s = 0;
                foreach (var item in sbd)
                {
                    s = int.Parse(item.ToString());
                }
                foreach (var item in tbv)
                {
                    t = item.ToString();
                }
                chuyenmuc.Tenchuyenmuc = t;
                chuyenmuc.Sobaiviet = s + 1;
                _context.Update(chuyenmuc);
                _context.Add(tintuc);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            ViewData["Machuyenmuc"] = new SelectList(_context.Chuyenmuc, "Tenchuyenmuc", "Tenchuyenmuc", tintuc.Machuyenmuc);
            return View(tintuc);
        }



        // GET: Tintucs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintuc.SingleOrDefaultAsync(m => m.Matintuc == id);
            if (tintuc == null)
            {
                return NotFound();
            }
            ViewData["Machuyenmuc"] = new SelectList(_context.Chuyenmuc, "Machuyenmuc", "Tenchuyenmuc", tintuc.Machuyenmuc);
            return View(tintuc);
        }

        // POST: Tintucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Matintuc,Tieude,Tieudecon,Noidung,Anh,Ngaydang,Tacgia,Machuyenmuc")] Tintuc tintuc, IFormFile Anh)
        {
            if (id != tintuc.Matintuc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Anh.Length > 0)
                    {
                        var uploadpath = Path.Combine(_enviroment.WebRootPath, "images");
                        Directory.CreateDirectory(Path.Combine(uploadpath));
                        string filename = Anh.FileName;
                        if (filename.Contains('\\'))
                        {
                            filename = filename.Split('\\').Last();
                        }
                        using (FileStream fileStream = new FileStream(Path.Combine(uploadpath, filename), FileMode.Create))
                        {
                            await Anh.CopyToAsync(fileStream);

                            tintuc.Anh = Anh.FileName;
                        }
                    }

                    _context.Update(tintuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TintucExists(tintuc.Matintuc))
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
            ViewData["Machuyenmuc"] = new SelectList(_context.Chuyenmuc, "Machuyenmuc", "Machuyenmuc", tintuc.Machuyenmuc);
            return View(tintuc);
        }

        // GET: Tintucs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintuc
                .Include(t => t.MachuyenmucNavigation)
                .SingleOrDefaultAsync(m => m.Matintuc == id);
            if (tintuc == null)
            {
                return NotFound();
            }

            return View(tintuc);
        }

        // POST: Tintucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tintuc = await _context.Tintuc.SingleOrDefaultAsync(m => m.Matintuc == id);
            _context.Tintuc.Remove(tintuc);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TintucExists(int id)
        {
            return _context.Tintuc.Any(e => e.Matintuc == id);
        }
    }
}
