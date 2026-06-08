using Microsoft.AspNetCore.Mvc;
//using PROG7311_POE_P2.Models;
using System.Diagnostics;
using System.Net.Http.Json;
using Web_API.Models;
using Web_API.Models.Client;
using Web_API.Models.Contract;
using Web_API.Models.DashboardViewModel;
using Web_API.Models.ServiceRequest;

namespace PROG7311_POE_P2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        // Helper method to easily generate our configured API client
        private HttpClient CreateApiClient() => _clientFactory.CreateClient("MyWebAPI");

        //================================================================
        // INDEX (Dashboard)
        //================================================================
        public async Task<IActionResult> Index()
        {
            var client = CreateApiClient();

            try
            {
                // Fire off concurrent requests to speed up performance
                var clientsTask = client.GetFromJsonAsync<List<Client>>("api/clients");
                var contractsTask = client.GetFromJsonAsync<List<Contract>>("api/contracts");
                var requestsTask = client.GetFromJsonAsync<List<ServiceRequest>>("api/servicerequests");

                await Task.WhenAll(clientsTask, contractsTask, requestsTask);

                var viewModel = new DashboardViewModel
                {
                    Clients = await clientsTask ?? new List<Client>(),
                    Contracts = await contractsTask ?? new List<Contract>(),
                    ServiceRequests = await requestsTask ?? new List<ServiceRequest>()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading dashboard data: {ex.Message}");

                // Return an explicit empty model structure so the webpage renders safely
                var emptyViewModel = new DashboardViewModel
                {
                    Clients = new List<Client>(),
                    Contracts = new List<Contract>(),
                    ServiceRequests = new List<ServiceRequest>()
                };

                return View(emptyViewModel);
            }
        }

        //================================================================
        // CREATE CLIENT   
        //================================================================
        [HttpGet] public IActionResult CreateClient() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClient(Client client)
        {
            if (ModelState.IsValid)
            {
                var httpClient = CreateApiClient();
                var response = await httpClient.PostAsJsonAsync("api/clients", client);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        //================================================================
        // CREATE CONTRACT
        //================================================================
        [HttpGet] public IActionResult CreateContract() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContract(Contract contract)
        {
            if (ModelState.IsValid)
            {
                var httpClient = CreateApiClient();
                var response = await httpClient.PostAsJsonAsync("api/contracts", contract);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        //================================================================
        // CREATE SERVICE REQUEST
        //================================================================
        [HttpGet] public IActionResult CreateServiceRequest() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateServiceRequest(ServiceRequest serviceRequest)
        {
            if (ModelState.IsValid)
            {
                var httpClient = CreateApiClient();
                var response = await httpClient.PostAsJsonAsync("api/servicerequests", serviceRequest);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(serviceRequest);
        }

        //================================================================
        // EDIT CLIENT
        //================================================================
        [HttpGet]
        public async Task<IActionResult> EditClient(int id)
        {
            try
            {
                var httpClient = CreateApiClient();
                var client = await httpClient.GetFromJsonAsync<Client>($"api/clients/{id}");
                return View(client);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Client ID {id} not found or API down: {ex.Message}");
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClient(int id, Client client)
        {
            if (id != client.ClientId) return BadRequest();

            if (ModelState.IsValid)
            {
                var httpClient = CreateApiClient();
                var response = await httpClient.PutAsJsonAsync($"api/clients/{id}", client);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        //================================================================
        // EDIT CONTRACT
        //================================================================
        [HttpGet]
        public async Task<IActionResult> EditContract(int id)
        {
            try
            {
                var httpClient = CreateApiClient();
                var contract = await httpClient.GetFromJsonAsync<Contract>($"api/contracts/{id}");
                return View(contract);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Contract ID {id} not found or API down: {ex.Message}");
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContract(int id, Contract contract)
        {
            if (id != contract.ContractId) return BadRequest();

            if (ModelState.IsValid)
            {
                var httpClient = CreateApiClient();
                var response = await httpClient.PutAsJsonAsync($"api/contracts/{id}", contract);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        //================================================================
        // EDIT SERVICE REQUEST
        //================================================================
        [HttpGet]
        public async Task<IActionResult> EditServiceRequest(int id)
        {
            try
            {
                var httpClient = CreateApiClient();
                var request = await httpClient.GetFromJsonAsync<ServiceRequest>($"api/servicerequests/{id}");
                return View(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Service request ID {id} not found or API down: {ex.Message}");
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditServiceRequest(int id, ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.ServiceRequestId) return BadRequest();

            if (ModelState.IsValid)
            {
                var httpClient = CreateApiClient();
                var response = await httpClient.PutAsJsonAsync($"api/servicerequests/{id}", serviceRequest);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(serviceRequest);
        }

        //================================================================
        // DELETE CLIENT
        //================================================================
        [HttpGet]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var httpClient = CreateApiClient();
                var client = await httpClient.GetFromJsonAsync<Client>($"api/clients/{id}");
                return View(client);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Client ID {id} cannot be read for deletion: {ex.Message}");
                return NotFound();
            }
        }

        [HttpPost, ActionName("DeleteClient")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteClientConfirmed(int id)
        {
            var httpClient = CreateApiClient();
            var response = await httpClient.DeleteAsync($"api/clients/{id}");

            if (!response.IsSuccessStatusCode)
                _logger.LogError($"Failed to delete client with ID: {id}");

            return RedirectToAction(nameof(Index));
        }

        //================================================================
        // DELETE CONTRACT
        //================================================================
        [HttpGet]
        public async Task<IActionResult> DeleteContract(int id)
        {
            try
            {
                var httpClient = CreateApiClient();
                var contract = await httpClient.GetFromJsonAsync<Contract>($"api/contracts/{id}");
                return View(contract);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Contract ID {id} cannot be read for deletion: {ex.Message}");
                return NotFound();
            }
        }

        [HttpPost, ActionName("DeleteContract")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteContractConfirmed(int id)
        {
            var httpClient = CreateApiClient();
            var response = await httpClient.DeleteAsync($"api/contracts/{id}");

            if (!response.IsSuccessStatusCode)
                _logger.LogError($"Failed to delete contract with ID: {id}");

            return RedirectToAction(nameof(Index));
        }

        //================================================================
        // DELETE SERVICE REQUEST
        //================================================================
        [HttpGet]
        public async Task<IActionResult> DeleteServiceRequest(int id)
        {
            try
            {
                var httpClient = CreateApiClient();
                var request = await httpClient.GetFromJsonAsync<ServiceRequest>($"api/servicerequests/{id}");
                return View(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Service Request ID {id} cannot be read for deletion: {ex.Message}");
                return NotFound();
            }
        }

        [HttpPost, ActionName("DeleteServiceRequest")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteServiceRequestConfirmed(int id)
        {
            var httpClient = CreateApiClient();
            var response = await httpClient.DeleteAsync($"api/servicerequests/{id}");

            if (!response.IsSuccessStatusCode)
                _logger.LogError($"Failed to delete service request with ID: {id}");

            return RedirectToAction(nameof(Index));
        }



        //================================================================


        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}