using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DNLiCore_Cache_Test.Models;
using Enyim.Caching;

namespace DNLiCore_Cache_Test.Controllers
{
    public class HomeController : Controller
    {
        // private IMemcachedClient _memcachedClient;
       
        public HomeController(IMemcachedClient memcachedClient)
        {
            //_memcachedClient = memcachedClient;
        }
        public IActionResult Index()
        {
            var _memcachedClient = DNLiCore_DI.ServiceContext.GetService<IMemcachedClient>();
            _memcachedClient.Set("123", DateTime.Now,100);
           var myData= _memcachedClient.Get("123");
           var myStats= _memcachedClient.Stats();
            return View();
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
