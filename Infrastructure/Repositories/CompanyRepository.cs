using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationConfiguration?> GetApplicationConfiguration(Guid companyId)
    {
        return await _context.ApplicationConfigurations.FirstOrDefaultAsync(ac => ac.CompanyId == companyId);
    }
}