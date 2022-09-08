using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using System.Collections.Concurrent;

namespace TestIdentityServer.Stores
{
    public class MyClientStore : IClientStore
    {
        private static readonly ConcurrentBag<Client> clients = new ConcurrentBag<Client>(new[]
        {
            new Client
                {
                    ClientId = "test-client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("AED9DB6A-0500-42C5-A932-0ADBE9B25FFA".Sha256()) },
                    AllowedScopes = { "scope1" }
                }
        });

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var result = clients.Where(c => c.ClientId == clientId)
                .SingleOrDefault();

            if (result == null)
                throw new ArgumentException($"Unknown client ID: {clientId}");

            return Task.FromResult(result);
        }

        public void AddClient(Client client)
        {
            clients.Add(client);
        }
    }
}
