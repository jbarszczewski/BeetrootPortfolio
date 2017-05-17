using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BeetrootPortfolio.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using BeetrootPortfolio.Configuration;

namespace BeetrootPortfolio.Data
{
    public class MongoDBProjectsRepository : IProjectsRepository
    {
        private PortfolioSettings settings;
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<Project> collection;

        public MongoDBProjectsRepository(IOptions<PortfolioSettings> settings)
        {
            this.settings = settings.Value;
            this.client = new MongoClient(this.settings.DatabaseEndpoint);
            this.database = this.client.GetDatabase(this.settings.DatabaseId);
            this.collection = this.database.GetCollection<Project>(this.settings.CollectionId);
        }

        public async Task<Project> CreateProjectAsync(Project item)
        {
            item.Id = Guid.NewGuid().ToString();
            await this.collection.InsertOneAsync(item);
            return item;
        }

        public async Task DeleteItemAsync(string id)
        {
            await this.collection.FindOneAndDeleteAsync(project => project.Id == id);
        }

        public async Task<Info> GetInfoAsync(string key)
        {
            var infoCollection = this.database.GetCollection<Info>(this.settings.InfoId);
            return await infoCollection.Find(info => info.Key == key).FirstOrDefaultAsync();
        }

        public async Task<Project> GetProjectAsync(string id)
        {
            return await this.collection.Find(project => project.Id == id).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(Expression<Func<Project, bool>> predicate)
        {
            return await this.collection.Find(predicate).ToListAsync();
        }

        public async Task<Project> UpdateItemAsync(string id, Project item)
        {
            return await this.collection.FindOneAndReplaceAsync(p => p.Id == id, item);
        }
    }
}
