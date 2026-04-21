using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_P2.Data;
using PROG7311_POE_P2.Models;
using PROG7311_POE_P2.Models.Client;
using PROG7311_POE_P2.Models.Contract;
using PROG7311_POE_P2.Models.ServiceRequest;
using PROG7311_POE_P2.Models.DashboardViewModel;
using System.Diagnostics;

namespace PROG7311_POE_P2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<HomeController> _logger;

        //================================================================
        // DB Context and Logger
        //================================================================
        public HomeController(ILogger<HomeController> logger, ApplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        //================================================================
        // INDEX
        //================================================================

        public IActionResult Index()
        {
            var viewModel = new DashboardViewModel
            {
                Clients = _context.Clients.ToList(),
                Contracts = _context.Contracts.ToList(),
                ServiceRequests = _context.ServiceRequests.ToList()
            };
            return View(viewModel);
        }

        //public IActionResult Index()
        //{
        //    // store data from the db to a var to pass to the view
        //    var data = _context.Clients.ToList();

        //    // pass the data to the view
        //    return View(data);
        //}

        //================================================================
        // CREATE
        //================================================================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Clients.Add(client);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        //================================================================
        // PRIVIACY
        //================================================================
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //================================================================

        [HttpGet]
        public IActionResult CreateContract() => View();

        [HttpGet]
        public IActionResult CreateServiceRequest() => View();

    }
}