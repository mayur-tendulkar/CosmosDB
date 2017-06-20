using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyTeaCompany.Model;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using System.Diagnostics;
using Microsoft.Azure.Documents.Linq;

namespace MyTeaCompany.Services
{
    class DocumentDBService : IDocumentDBService
    {
        public List<StoreInfo> Items { get; private set; }

        DocumentClient client;
        Uri collectionLink;

        public DocumentDBService()
        {
            client = new DocumentClient(new Uri(Constants.EndpointUri), Constants.PrimaryKey);
            collectionLink = UriFactory.CreateDocumentCollectionUri(Constants.DatabaseName, Constants.CollectionName);
        }
        public async Task CreateDatabaseAsync(string databaseName)
        {
            try
            {
                await client.CreateDatabaseIfNotExistsAsync(new Database
                {
                    Id = databaseName
                });
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }

        }

        public async Task CreateDocumentCollectionAsync(string databaseName, string collectionName)
        {
            try
            {
                // Create collection with 400 RU/s
                await client.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(Constants.DatabaseName),
                    new DocumentCollection
                    {
                        Id = collectionName
                    },
                    new RequestOptions
                    {
                        OfferThroughput = 400
                    });
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }

        }

        public async Task DeleteStoreInfoAsync(string id)
        {
            try
            {
                await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(Constants.DatabaseName, Constants.CollectionName, id));
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }
        }

        async Task DeleteDocumentCollection()
        {
            try
            {
                await client.DeleteDocumentCollectionAsync(collectionLink);
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }
        }

        async Task DeleteDatabase()
        {
            try
            {
                await client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(Constants.DatabaseName));
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }

        }

        public async Task<List<StoreInfo>> GetStoreInfoAsync()
        {
            Items = new List<StoreInfo>();

            try
            {
                var query = client.CreateDocumentQuery<StoreInfo>(collectionLink)
                    .AsDocumentQuery();
                while (query.HasMoreResults)
                {
                    Items.AddRange(await query.ExecuteNextAsync<StoreInfo>());
                }
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }

            return Items;
        }

        public async Task SaveStoreInfoAsync(StoreInfo store, bool isNewItem)
        {
            try
            {
                if (isNewItem)
                {
                    await client.CreateDocumentAsync(collectionLink, store);
                }
                else
                {
                    await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(Constants.DatabaseName, Constants.CollectionName, store.Id), store);
                }
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }

        }
    }
}
