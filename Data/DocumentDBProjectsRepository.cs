using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

using BeetrootPortfolio.Models;
using BeetrootPortfolio.Configuration;
using Microsoft.Extensions.Options;

namespace BeetrootPortfolio.Data
{
    public class DocumentDBProjectsRepository : IProjectsRepository
    {
        private PortfolioSettings settings;
        private DocumentClient client;

        public DocumentDBProjectsRepository(IOptions<PortfolioSettings> settings)
        {
            this.settings = settings.Value;
            this.client = new DocumentClient(new Uri(this.settings.DatabaseEndpoint), this.settings.DatabaseKey);
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        public async Task<Project> GetProjectAsync(string id)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(this.settings.DatabaseId, this.settings.CollectionId, id));
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
                UriFactory.CreateDocumentCollectionUri(this.settings.DatabaseId, this.settings.CollectionId),
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
            var document = await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(this.settings.DatabaseId, this.settings.CollectionId), item);
            return (Project)(dynamic)document;
        }

        public async Task<Project> UpdateItemAsync(string id, Project item)
        {
            var document = await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(this.settings.DatabaseId, this.settings.CollectionId, id), item);
            return (Project)(dynamic)document;
        }

        public async Task DeleteItemAsync(string id)
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(this.settings.DatabaseId, this.settings.CollectionId, id));
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(this.settings.DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = this.settings.DatabaseId });
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
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(this.settings.DatabaseId, this.settings.CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(this.settings.DatabaseId),
                        new DocumentCollection { Id = this.settings.CollectionId },
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