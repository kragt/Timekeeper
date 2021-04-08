using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Timekeeper.Models;

namespace Timekeeper.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Project>> GetProjectsAsync(string query);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project> GetProjectAsync(string projectId);
        Task AddProjectAsync(Project project);
        Task UpdateProjectAsync(string projectId, Project project);
        Task DeleteProjectAsync(string projectId);

        Task<IEnumerable<ProjectSlice>> GetSlicesAsync(string query);
        Task<ProjectSlice> GetSliceAsync(string sliceId);
        Task AddSliceAsync(ProjectSlice slice);
        Task UpdateSliceAsync(string sliceId, ProjectSlice slice);
        Task DeleteSliceAsync(string sliceId);
    }

    public class CosmosDbService : ICosmosDbService
    {
        private readonly Container _container;

        public CosmosDbService(CosmosClient db, string database, string container)
        {
            this._container = db.GetContainer(database, container);
        }

        public async Task AddProjectAsync(Project project)
        {
            await this._container.CreateItemAsync<Project>(project, new PartitionKey(project.ProjectId));
        }

        public async Task AddSliceAsync(ProjectSlice slice)
        {
            await this._container.CreateItemAsync<ProjectSlice>(slice, new PartitionKey(slice.ProjectId));
        }

        public async Task DeleteProjectAsync(string projectId)
        {
            await this._container.DeleteItemAsync<Project>(projectId, new PartitionKey(projectId));
        }

        public Task DeleteSliceAsync(string sliceId)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetProjectAsync(string projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(string query)
        {
            var q = this._container.GetItemQueryIterator<Project>(new QueryDefinition(query));
            List<Project> results = new List<Project>();
            while (q.HasMoreResults)
            {
                var response = await q.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await GetProjectsAsync("SELECT * FROM c WHERE c.type=\"project\"");
        }

        public Task<ProjectSlice> GetSliceAsync(string sliceId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectSlice>> GetSlicesAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProjectAsync(string projectId, Project project)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSliceAsync(string sliceId, ProjectSlice slice)
        {
            throw new NotImplementedException();
        }
    }
}
