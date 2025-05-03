using Artefact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Services.Api
{
    public interface IApiClient
    {
        Task<IEnumerable<CollectionModel>> GetAllCollectionsAsync();
        Task<IEnumerable<CollectionModel>> GetUserCollectionsAsync(int userId);
        Task<IEnumerable<CollectionModel>> GetPermittedCollectionsAsync(int userId);
        Task GrantCollectionPermissionAsync(int collectionId, int userId);
    }
}
