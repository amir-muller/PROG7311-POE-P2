using Web_API.Models.Client;
using Web_API.Models.Contract;
using Web_API.Models.ServiceRequest;

namespace Web_API.Models.DashboardViewModel;

public class DashboardViewModel
{
    public IEnumerable<Web_API.Models.Client.Client> Clients { get; set; } = new List<Web_API.Models.Client.Client>();
    public IEnumerable<Web_API.Models.Contract.Contract> Contracts { get; set; } = new List<Web_API.Models.Contract.Contract>();
    public IEnumerable<Web_API.Models.ServiceRequest.ServiceRequest> ServiceRequests { get; set; } = new List<Web_API.Models.ServiceRequest.ServiceRequest>();

}
