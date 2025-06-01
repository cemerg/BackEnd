namespace Application.Interfaces;

public interface ICompanyService
{
    Task<ApplicationConfigurationDto?> GetApplicationConfiguration(Guid companyId);
}
