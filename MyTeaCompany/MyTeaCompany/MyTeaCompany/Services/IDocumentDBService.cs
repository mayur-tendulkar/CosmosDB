using MyTeaCompany.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTeaCompany.Services
{
    public interface IDocumentDBService
    {
        Task CreateDatabaseAsync(string databaseName);

        Task CreateDocumentCollectionAsync(string databaseName, string collectionName);

        Task<List<StoreInfo>> GetStoreInfoAsync();

        Task SaveStoreInfoAsync(StoreInfo store, bool isNewItem);

        Task DeleteStoreInfoAsync(string id);
    }

}
