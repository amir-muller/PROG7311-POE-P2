using Microsoft.EntityFrameworkCore;

namespace Web_API.Data;

public class ApplicationDBContext: DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    }

    // defined DBSets for the models
    public DbSet<Models.Client.Client> Clients { get; set; }
    public DbSet<Models.Contract.Contract> Contracts { get; set; }
    public DbSet<Models.ServiceRequest.ServiceRequest> ServiceRequests { get; set; }
}
