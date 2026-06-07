//using Microsoft.AspNetCore.Mvc;
//using Web_API.Models;
//using Web_API.Models.Client;
//using Web_API.Models.Contract;
//using Web_API.Models.DashboardViewModel;
//using Web_API.Models.ServiceRequest;
//using System.Diagnostics;
//using System.Net.Http.Json;


//namespace PROG7311_POE_P2.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly IHttpClientFactory _clientFactory;
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
//        {
//            _logger = logger;
//            _clientFactory = clientFactory;
//        }

//        private HttpClient CreateClient() => _clientFactory.CreateClient("MyWebAPI");

//        //================================================================
//        // INDEX (Dashboard)
//        //================================================================
//        public async Task<IActionResult> Index()
//        {
//            var client = CreateClient();

//            // Fire off API calls concurrently to speed up the dashboard load
//            var clientsTask = client.GetFromJsonAsync<List<Client>>("api/clients");
//            var contractsTask = client.GetFromJsonAsync<List<Contract>>("api/contracts");
//            var requestsTask = client.GetFromJsonAsync<List<ServiceRequest>>("api/servicerequests");

//            await Task.WhenAll(clientsTask, contractsTask, requestsTask);

//            var viewModel = new DashboardViewModel
//            {
//                Clients = await clientsTask ?? new List<Client>(),
//                Contracts = await contractsTask ?? new List<Contract>(),
//                ServiceRequests = await requestsTask ?? new List<ServiceRequest>()
//            };

//            return View(viewModel);
//        }

//        //================================================================
//        // CREATE CLIENT   
//        //================================================================
//        [HttpGet] public IActionResult CreateClient() => View();

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> CreateClient(Client client)
//        {
//            if (ModelState.IsValid)
//            {
//                var httpClient = CreateClient();
//                var response = await httpClient.PostAsJsonAsync("api/clients", client);

//                if (response.IsSuccessStatusCode)
//                    return RedirectToAction(nameof(Index));
//            }
//            return View(client);
//        }

//        //================================================================
//        // EDIT CLIENT
//        //================================================================
//        [HttpGet]
//        public async Task<IActionResult> EditClient(int id)
//        {
//            var httpClient = CreateClient();
//            var client = await httpClient.GetFromJsonAsync<Client>($"api/clients/{id}");
//            if (client == null) return NotFound();
//            return View(client);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> EditClient(int id, Client client)
//        {
//            if (ModelState.IsValid)
//            {
//                var httpClient = CreateClient();
//                var response = await httpClient.PutAsJsonAsync($"api/clients/{id}", client);

//                if (response.IsSuccessStatusCode)
//                    return RedirectToAction(nameof(Index));
//            }
//            return View(client);
//        }

//        //================================================================
//        // DELETE CLIENT
//        //================================================================
//        [HttpGet]
//        public async Task<IActionResult> DeleteClient(int id)
//        {
//            var httpClient = CreateClient();
//            var client = await httpClient.GetFromJsonAsync<Client>($"api/clients/{id}");
//            if (client == null) return NotFound();
//            return View(client);
//        }

//        [HttpPost, ActionName("DeleteClient")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteClientConfirmed(int id)
//        {
//            var httpClient = CreateClient();
//            var response = await httpClient.DeleteAsync($"api/clients/{id}");

//            return RedirectToAction(nameof(Index));
//        }

//        // (Repeat this exact pattern for Contracts and Service Requests!)

//        public IActionResult Privacy() => View();

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}