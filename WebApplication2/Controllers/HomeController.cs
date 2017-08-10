using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{


    public class HomeController : Controller
    {
        private readonly WebTTContext _context;

        public HomeController(WebTTContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View($"Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind("Hoten,Taikhoan,Matkhau,Email")] User user)
        {
            if (_context.User.Any(x => x.Taikhoan == user.Taikhoan))
                ViewData["Error"] = "Tài khoản đã tồn tại trong hệ thống";
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    _context.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }


        public IActionResult Login(string returnUrl = null)
        {

            if (HttpContext.Session.GetString("SessionName") != null)
            {
                ViewData["ReturnUrl"] = returnUrl;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(User ser, bool isloginAdmin = false)
        {
            //ViewData["ReturnUrl"] = ReturnUrl;
            var user = _context.User.FirstOrDefault(u => u.Taikhoan == ser.Taikhoan && u.Matkhau == ser.Matkhau);
            if (user == null)
            {
                ViewData["Error"] = "Tài khoản nhập vào không tồn tại";
                return View();
            }
            else
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, user.Manguoidung.ToString()),
                    new Claim(ClaimTypes.Name, user.Taikhoan)
                };

                ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims));
                await HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", principal, new AuthenticationProperties()
                {
                    IsPersistent = false //remember login
                });
                if (user.Groupid == "ADMIN" || user.Groupid == "MOD")
                {
                    //if (!user.Matkhau.Equals(matkhau)) return new BadRequestObjectResult(ModelState);
                    HttpContext.Session.SetInt32("Sessionid", user.Manguoidung);
                    HttpContext.Session.SetString("SessionName", user.Taikhoan);
                    HttpContext.Session.SetString("SessionAdmin", user.Groupid);
                    var ident=new ClaimsIdentity(new []
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Taikhoan),
                        new Claim("http://schemas.microsoft.com/accesscontrolservice/2017/claims/identiyprovider","ASP.NET Identity","http://w3.org/2001/XMLSchema#string"),
                        new Claim(ClaimTypes.Name,user.Taikhoan),}
                    );
                    
                    //HttpContext.Session.SetString("SessionName", user.IsSupper.ToString());
                    return RedirectToAction("Index", "Tintucs");
                }
                else
                {
                    HttpContext.Session.SetInt32("Sessionid", user.Manguoidung);
                    HttpContext.Session.SetString("SessionAdmin", user.Groupid);
                    HttpContext.Session.SetString("SessionName", user.Taikhoan);
                    //HttpContext.Session.SetString("SessionName", user.IsSupper.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult LogOffSession()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
