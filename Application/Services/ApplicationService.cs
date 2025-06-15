using Application.Interfaces;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<ApplicationConfigurationDto?> GetApplicationConfiguration()
    {
        var applicationConfiguration = await _applicationRepository.GetApplicationConfiguration();
        return new ApplicationConfigurationDto()
        {
            ApplicationTitle = applicationConfiguration?.ApplicationTitle ?? "Default Application Title",
        };
    }
}