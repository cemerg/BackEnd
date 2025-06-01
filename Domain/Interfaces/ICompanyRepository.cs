
using Domain.Entities;

public interface ICompanyRepository
{
    Task<ApplicationConfiguration?> GetApplicationConfiguration(Guid companyId);
}