using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

using BeetrootPortfolio.Models;

namespace BeetrootPortfolio.Data
{
    public class DocumentDBProjectsRepository : IProjectsRepository
    {
        private string databaseId;
        private string collectionId;
        private DocumentClient client;

        public DocumentDBProjectsRepository(string endpoint, string key, string databaseId, string collectionId)
        {
            this.databaseId = databaseId;
            this.collectionId = collectionId;
            this.client = new DocumentClient(new Uri(endpoint), key);
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        public async Task<Project> GetProjectAsync(string id)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, id));
                return (Project)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(Expression<Func<Project, bool>> predicate)
        {
            IDocumentQuery<Project> query = client.CreateDocumentQuery<Project>(
                UriFactory.CreateDocumentCollectionUri(databaseId, collectionId),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();

            List<Project> results = new List<Project>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<Project>());
            }

            return results;
        }

        public async Task<Project> CreateProjectAsync(Project item)
        {
            var document = await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), item);
            return (Project)(dynamic)document;
        }

        public async Task<Project> UpdateItemAsync(string id, Project item)
        {
            var document = await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, id), item);
            return (Project)(dynamic)document;
        }

        public async Task DeleteItemAsync(string id)
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, id));
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = databaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(databaseId),
                        new DocumentCollection { Id = collectionId },
                        new RequestOptions { OfferThroughput = 400 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}