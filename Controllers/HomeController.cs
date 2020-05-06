using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Calendar.Models;
using Calendar.Service;

namespace Calendar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DataReaderService dataReaderService = new DataReaderService();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
    
            //return Json(dataReaderService.getEvents());
        }

        public IActionResult removeEvent(string name, string time)
        {
            dataReaderService.delEvent(name);

            return RedirectToAction("Index");
        }
        public IActionResult GetEvents()
        {
            return Json(dataReaderService.getEventsForCalendar());
        }
        [Route("home/event/{date}")]
        public IActionResult Event(string date)
        {
            ViewBag.events = dataReaderService.getEventsWithDate(date);
            ViewBag.date = date;
            return View();
        }

        [Route("home/edit/{name}")]
        public IActionResult Edit(string name)
        {
            ViewBag.ev = dataReaderService.findWithName(name);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(string titleInput, string hourInput, string timeInput)
        {
            dataReaderService.saveToFile();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
