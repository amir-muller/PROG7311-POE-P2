namespace PROG7311_POE_P2.Models.Contract
{
    public class Contract
    {
        public int ContractId { get; set; }
        public int ClientId { get; set; }
        public DateOnly EndDate { get; set; }
        public DateOnly StartDate { get; set; }
        public string ServiceLevel { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
