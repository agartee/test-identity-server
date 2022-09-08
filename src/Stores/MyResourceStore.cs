using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using System.Collections.Concurrent;

namespace TestIdentityServer.Stores
{
    public class MyResourceStore : IResourceStore
    {
        private readonly ConcurrentBag<IdentityResource> _identityResources = new ConcurrentBag<IdentityResource>(new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            });

        private readonly ConcurrentBag<ApiResource> _apiResources = new ConcurrentBag<ApiResource>();

        private readonly ConcurrentBag<ApiScope> _apiScopes = new ConcurrentBag<ApiScope>(new []
            {
                    new ApiScope("scope1"),
                    new ApiScope("scope2"),
            });

        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            if (apiResourceNames == null) throw new ArgumentNullException(nameof(apiResourceNames));

            var query = from a in _apiResources
                        where apiResourceNames.Contains(a.Name)
                        select a;

            return Task.FromResult(query);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

            var query = from a in _apiResources
                        where a.Scopes.Any(x => scopeNames.Contains(x))
                        select a;

            return Task.FromResult(query);
        }

        public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

            var query =
                from x in _apiScopes
                where scopeNames.Contains(x.Name)
                select x;

            return Task.FromResult(query);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

            var identity = from i in _identityResources
                           where scopeNames.Contains(i.Name)
                           select i;

            return Task.FromResult(identity);
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            var result = new Resources(_identityResources, _apiResources, _apiScopes);
            return Task.FromResult(result);
        }
    }
}
