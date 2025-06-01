using Application.Interfaces;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<ApplicationConfigurationDto?> GetApplicationConfiguration(Guid companyId)
    {
        var applicationConfiguration = await _companyRepository.GetApplicationConfiguration(companyId);
        return new ApplicationConfigurationDto()
        {
            ApplicationTitle = applicationConfiguration?.ApplicationTitle ?? "Default Application Title",
        };
    }
}