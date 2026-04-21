using PROG7311_POE_P2.Models.Client;
using PROG7311_POE_P2.Models.Contract;
using PROG7311_POE_P2.Models.ServiceRequest;

namespace PROG7311_POE_P2.Models.DashboardViewModel;

public class DashboardViewModel
{
    public IEnumerable<PROG7311_POE_P2.Models.Client.Client> Clients { get; set; }
    public IEnumerable<PROG7311_POE_P2.Models.Contract.Contract> Contracts { get; set; }
    public IEnumerable<PROG7311_POE_P2.Models.ServiceRequest.ServiceRequest> ServiceRequests { get; set; }

}
