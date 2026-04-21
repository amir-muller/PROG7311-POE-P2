using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_P2.Data;
using PROG7311_POE_P2.Models;
using PROG7311_POE_P2.Models.Client;
using PROG7311_POE_P2.Models.Contract;
using PROG7311_POE_P2.Models.ServiceRequest;
using PROG7311_POE_P2.Models.DashboardViewModel;
using System.Diagnostics;
using PROG7311_POE_P2.Services;

namespace PROG7311_POE_P2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly CurrencyService _currencyService;

        //================================================================
        // DB Context and Logger
        //================================================================
        public HomeController(ILogger<HomeController> logger, ApplicationDBContext context, CurrencyService currencyService)
        {
            _logger = logger;
            _context = context;
            _currencyService = currencyService;
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
        // CREATE CLIENT   
        //================================================================
        [HttpGet]
        public IActionResult CreateClient() => View();
      
        // post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateClient(Client client)
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
        // CREATE CONTRACT
        //================================================================

        [HttpGet]
        public IActionResult CreateContract() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateContract(Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Contracts.Add(contract);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        //================================================================
        // CREATE SERVICE REQUEST
        //================================================================

        [HttpGet]
        public IActionResult CreateServiceRequest() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateServiceRequest(ServiceRequest serviceRequest, decimal amountInUSD)
        {
           decimal randAmount = await _currencyService.ConvertUsdToZar(amountInUSD);

            serviceRequest.Cost = randAmount;

            if (ModelState.IsValid)
            {
                _context.ServiceRequests.Add(serviceRequest);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceRequest);
        }


        //public IActionResult CreateServiceRequest(ServiceRequest serviceRequest)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.ServiceRequests.Add(serviceRequest);
        //        _context.SaveChanges();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(serviceRequest);
        //}



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

       

    }
}