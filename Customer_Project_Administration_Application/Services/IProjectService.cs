using Projekt2_tidrapportering.DTOS.ProjectDTOS;

namespace Customer_Project_Administration_Application.Services;

public interface IProjectService
{
    public List<ProjectsDTO> GetProjects();
    public Status CreateProject(CreateProjectDTO project);
    public Status UpdateProject(UpdateProjectDTO project, int Id);
    public Status DeleteProject(int Id);
        
}