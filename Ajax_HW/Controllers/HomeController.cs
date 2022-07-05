using Ajax_HW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajax_HW.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly DemoContext _context;
                                                                //↓相當於new了DemoContext，要維護直接在startup裡面增加方便性
        public HomeController(ILogger<HomeController> logger, DemoContext conetxt)
        {

            _logger = logger;
            _context = conetxt;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            
            return View();
        }

        public IActionResult CheckMember(string name)
        {
            var nameExist = _context.Members.Any(m => m.Name == name);

                return Content(nameExist.ToString(),"text/plain",Encoding.UTF8);
            
        }

        public IActionResult Address()
        {
            return View();
        }


        public IActionResult City()
        {
            var cities = _context.Addresses.Select(a => a.City).Distinct();
            return Json(cities);
        }
        public IActionResult Districts(string city)
        {
            var districts = _context.Addresses.Where(a => a.City == city).Select(d => d.SiteId).Distinct();
            return Json(districts);
        }
        public IActionResult Roads(string district)
        {
            var roads = _context.Addresses.Where(a => a.SiteId == district).Select(r => r.Road);
            return Json(roads);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
