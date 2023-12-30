using costs.Domain.APIModel;
using costs.Domain.Model;
using costs.Domain.Interfaces;
using Microsoft.VisualBasic.FileIO;
using MongoDB.Driver;

namespace costs.Repository
{
    public interface IProjectRepository
    {
        public IList<Project> GetProjects();
        public Project GetProjectById(string projectId);
        public void AddProject(Project project);
        public void DeleteProject(string projectId);


    }
    public class ProjectRepository : IProjectRepository
    {
        private readonly IMongoCollection<Project> _collection;

        public ProjectRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _collection = database.GetCollection<Project>("Projects");
        }

        public IList<Project> GetProjects()
        {
            return _collection.Find("{}").ToList();
        }
        public Project GetProjectById(string projectId)
        {
            var filter = Builders<Project>.Filter.Eq(x => x.Id, projectId);

            return _collection.Find(filter).FirstOrDefault();
        }

        public void AddProject(Project project)
        {
            _collection.InsertOne(project);
        }

        public void UpdateProject(Project project)
        {
            var filter = Builders<Project>.Filter.Eq(x => x.Id, project.Id);

            var update = Builders<Project>.Update
                .Set(proj => proj.Name, project.Name)
                .Set(proj => proj.Budget, project.Budget)
                .Set(proj => proj.Cost, project.Cost)
                .Set(proj => proj.Category, project.Category)
                .Set(proj => proj.Services, project.Services);

            _collection.UpdateOne(filter,update);
        }
        
        public void DeleteProject(string projectId)
        {
            var filter = Builders<Project>.Filter.Eq(x => x.Id, projectId);

            _collection.DeleteOne(filter);
        }
    }
}