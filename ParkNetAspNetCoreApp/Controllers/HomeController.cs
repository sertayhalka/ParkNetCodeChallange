using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkNetAspNetCoreApp.Models;
using System;
using System.Linq;
using System.Xml.Linq;

namespace ParkNetAspNetCoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            var doc =  XDocument.Load(@"C:\Users\ataha\Documents\words.xml");

            var result = doc.Descendants("Word").Where(x => x.Attribute("Text") != null).Select(x => new WordsViewModel
            {
                Text = x.Attribute("Text").Value,
                Count = Convert.ToInt16(x.Attribute("Count").Value)
            }).OrderByDescending(x => x.Count).Take(10);

            return View(result);
        }

    }
}
