using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Projekt2_tidrapportering.DTOS.ProjectDTOS;

namespace Customer_Project_Administration_Application.Services
{
    public class ProjectSettings
    {
        public string Url { get; set; }
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

        public Status CreateProject(CreateProjectDTO project)
        {
            var payload = JsonConvert.SerializeObject(project);
            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            var data = httpClient.PostAsync(_settings.Value.Url, httpContent).Result;
            if (data.IsSuccessStatusCode)
            {
                return Status.ok;
            }

            return Status.Error;
        }

        public Status UpdateProject(UpdateProjectDTO project, int Id)
        {
            var payload = JsonConvert.SerializeObject(project);
            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            var data = httpClient.PutAsync($"{_settings.Value.Url}/{Id}", httpContent).Result;
            if (data.IsSuccessStatusCode)
            {
                return Status.ok;
            }

            return Status.Error;
        }

        public Status DeleteProject(int Id)
        {
            var httpClient = new HttpClient();
            var data = httpClient.DeleteAsync($"{_settings.Value.Url}/{Id}").Result;
            if (data.IsSuccessStatusCode)
            {
                return Status.ok;
            }

            return Status.Error;
        }
    }
}