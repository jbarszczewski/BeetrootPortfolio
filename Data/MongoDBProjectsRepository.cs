using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BeetrootPortfolio.Models;
using MongoDB.Driver;

namespace BeetrootPortfolio.Data
{
    public class MongoDBProjectsRepository : IProjectsRepository
    {
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<Project> collection;
        string databaseId;
        string collectionId;

        public MongoDBProjectsRepository(string connectionString, string databaseId, string collectionId)
        {
            this.databaseId = databaseId;
            this.collectionId = collectionId;
            this.client = new MongoClient(connectionString);
            this.database = this.client.GetDatabase(this.databaseId);
            this.collection = this.database.GetCollection<Project>(this.collectionId);
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
