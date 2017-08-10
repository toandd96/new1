using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Text.Encodings.Web;


namespace WebApplication2.Controllers
{
    [Authorize]
    public class TintucsController : Controller
    {
        HtmlEncoder _htmlEncoder;
        readonly JavaScriptEncoder _javaScriptEncoder;
        UrlEncoder _urlEncoder;

        private readonly WebTTContext _context;
        private readonly IHostingEnvironment _enviroment;


        public TintucsController(WebTTContext context, IHostingEnvironment enviroment, HtmlEncoder htmlEncoder,
                             JavaScriptEncoder javascriptEncoder,
                             UrlEncoder urlEncoder)
        {
            _htmlEncoder = htmlEncoder;
            _javaScriptEncoder = javascriptEncoder;
            _urlEncoder = urlEncoder;
            _enviroment = enviroment;
            _context = context;

        }
        public JsonResult Getalltintuc()
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

        //[ValidateInput(false)]
        public IActionResult Index()
        {
            
                
                var webTtContext = _context.Tintuc.Include(t => t.MachuyenmucNavigation);
                //HttpContext.Session.SetInt32("a", 5);
                return View(webTtContext);
            
        }

        // GET: Tintucs/Details/5

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

        public IActionResult Create()
        {
            ViewData["Machuyenmuc"] = new SelectList(_context.Chuyenmuc, "Machuyenmuc", "Tenchuyenmuc");
            return View();
        }

        // POST: Tintucs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matintuc,Tieude,Tieudecon,Noidung,Anh,Ngaydang,Tacgia,Machuyenmuc")] Tintuc tintuc, IFormFile Anh, Chuyenmuc chuyenmuc)
        {
            if (ModelState.IsValid)
            {
                if (Anh != null && Anh.Length > 0)
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
                var sbv = await _context.Chuyenmuc.SingleOrDefaultAsync(c => c.Machuyenmuc == tintuc.Machuyenmuc);
                var encode = _javaScriptEncoder.Encode(tintuc.Noidung);

                tintuc.Noidung = encode;
                _context.Add(tintuc);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            ViewData["Machuyenmuc"] = new SelectList(_context.Chuyenmuc, "Tenchuyenmuc", "Tenchuyenmuc", tintuc.Machuyenmuc);
            return View(tintuc);
        }


        public ActionResult LogoffSession()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        // GET: Tintucs/Edit/5

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

        public async Task<IActionResult> Edit(int? id, [Bind("Matintuc,Tieude,Tieudecon,Noidung,Anh,Ngaydang,Tacgia,Machuyenmuc")] Tintuc tintuc, IFormFile Anh, Chuyenmuc chuyenmuc)
        {

            if (id != tintuc.Matintuc)
            {
                return NotFound();
            }
            var a = tintuc.Machuyenmuc;
            if (ModelState.IsValid)
            {
                try
                {
                    Tintuc gettintuc = await _context.Tintuc.AsNoTracking().SingleOrDefaultAsync(t => t.Matintuc == id);
                    var getimage = gettintuc.Anh;
                    // sửa ảnh cho tin tức
                    if (Anh == null || Anh.Length < 0)
                    {
                        tintuc.Anh = getimage;
                    }
                    else
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
                    //lấy tin tức trước khi sửa
                    Tintuc tintucchuasua = await _context.Tintuc.AsNoTracking().Where(tt => tt.Matintuc == id).FirstOrDefaultAsync();
                    //số bài viết giảm đi trong chuyên mục
                    Chuyenmuc chuyenmucbisua = await _context.Chuyenmuc.SingleOrDefaultAsync(c => c.Machuyenmuc == tintucchuasua.Machuyenmuc);
                    //chuyenmucbisua.Sobaiviet = chuyenmucbisua.Sobaiviet - 1;
                    // sửa số bài viết tăng lên trong chuyên mục
                    chuyenmuc = await _context.Chuyenmuc.SingleOrDefaultAsync(c => c.Machuyenmuc == tintuc.Machuyenmuc);
                    //chuyenmuc.Sobaiviet = chuyenmuc.Sobaiviet + 1;
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


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var a = HttpContext.Session.GetInt32("a");
            ViewData["a"] = a;
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

        public async Task<IActionResult> DeleteConfirmed(int id, Chuyenmuc cm)
        {

            var tintuc = await _context.Tintuc.SingleOrDefaultAsync(am => am.Matintuc == id);
            var chuyenmuc = await _context.Chuyenmuc.SingleOrDefaultAsync(c => c.Machuyenmuc == tintuc.Machuyenmuc);

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