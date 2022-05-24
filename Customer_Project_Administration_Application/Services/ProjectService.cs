using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Projekt2_tidrapportering.DTOS.ProjectDTOS;

namespace Customer_Project_Administration_Application.Services
{
    public interface IProjectService
    {
        public List<ProjectsDTO> GetProjects();
    }
    public class ProjectService : IProjectService
    {
        private readonly IOptions<ProjectSettings> _settings;

        public ProjectService(IOptions<ProjectSettings> settings)
        {
            _settings = settings;
        }
        public List<ProjectsDTO> GetProjects()
        {
            var httpClient = new HttpClient();
            var data =
                httpClient.GetStringAsync(_settings.Value.Url).Result;

            return JsonConvert.DeserializeObject<List<ProjectsDTO>>(data);
        }
    }

    public class ProjectSettings
    {
        public string Url { get; set; }
    }
}
