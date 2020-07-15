using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClaimsRUs.Data.ViewModels;
using ClaimsRUs.Data.Abstractions.Readers;
using ClaimsRUs.Data.Abstractions.Models;

namespace ClaimsRUs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehiclesReader vehiclesReader;

        public HomeController(ILogger<HomeController> logger, IVehiclesReader vehiclesReader)
        {
            _logger = logger;
            this.vehiclesReader = vehiclesReader;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Vehicles()
        {
            IEnumerable<IVehicle> _vehicleList = vehiclesReader.ReadAll();

            return View(_vehicleList);
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
