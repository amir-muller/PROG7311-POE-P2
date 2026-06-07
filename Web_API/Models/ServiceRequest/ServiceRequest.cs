namespace Web_API.Models.ServiceRequest
{
    public class ServiceRequest
    {
        public int ServiceRequestId { get; set; }
        public int ContractId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string Status { get; set; }
    }
}
