using ERP.Application.Interfaces.Helpers;
using Microsoft.Extensions.Configuration;

namespace ERP.Application.Helpers
{
    public class DirectoryPathService : IDirectoryPathService
    {
        private readonly IConfiguration _configuration;

        public DirectoryPathService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Directory =>
            _configuration["Storage:PeopleDirectory"]
            ?? throw new InvalidOperationException(
                "People storage directory is not configured.");
    }
}
