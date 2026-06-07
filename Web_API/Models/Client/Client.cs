using Microsoft.EntityFrameworkCore;

namespace Web_API.Models.Client
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string contactDetails { get; set; }
        public string Region { get; set; }
    }
}
