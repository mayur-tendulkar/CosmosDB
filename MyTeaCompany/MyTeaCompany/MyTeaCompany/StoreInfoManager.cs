using MyTeaCompany.Model;
using MyTeaCompany.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTeaCompany
{
    public class StoreInfoManager
    {
        IDocumentDBService documentDBService;

        public StoreInfoManager(IDocumentDBService service)
        {
            documentDBService = service;
        }

        public Task CreateDatabase(string databaseName)
        {
            return documentDBService.CreateDatabaseAsync(databaseName);
        }

        public Task CreateDocumentCollection(string databaseName, string collectionName)
        {
            return documentDBService.CreateDocumentCollectionAsync(databaseName, collectionName);
        }

        public Task<List<StoreInfo>> GetStoreInfoAsync()
        {
            return documentDBService.GetStoreInfoAsync();
        }

        public Task SaveStoreInfoAsync(StoreInfo store, bool isNewItem = false)
        {
            return documentDBService.SaveStoreInfoAsync(store, isNewItem);
        }

        public Task DeleteStoreInfoAsync(StoreInfo store)
        {
            return documentDBService.DeleteStoreInfoAsync(store.Id);
        }

    }
}
