using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Sequences.Data.Clients;

namespace Sequences.Services.Clients
{
    public class ClientServices : IClientsService
    {
        #region Private Felds

        private readonly ILogger<ClientServices> _logger;
        private readonly IClientRepository _repository;

        #endregion Private Felds

        #region Constructors

        public ClientServices(ILogger<ClientServices> logger, IClientRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #endregion Constructors

        #region Public Methods

        public async Task<List<Client>> GetClients()
        {
            var dbClients = await _repository.GetAll();
            var clients = dbClients
                .Select(c => new Client(c.ClientId, c.Name, c.Description))
                .ToList();

            return clients;
        }

        public async Task<Client> GetClientById(int id)
        {
            var client = await _repository.GetById(id);

            return new Client(client.ClientId, client.Name, client.Description);
        }

        public async Task<Client> Add(Client client)
        {
            var dbClient = new Data.Clients.Entities.Client(client.Id, client.Name, client.Description);
            var _client = await _repository.Add(dbClient);
            (client.Id, client.Name, client.Description) = (_client.ClientId, _client.Name, _client.Description);

            return client;
        }

        public async Task<Client> Update(Client client)
        {
            var dbClient = new Data.Clients.Entities.Client(client.Id, client.Name, client.Description);
            var _client = await _repository.Update(dbClient);
            (client.Id, client.Name, client.Description) = (_client.ClientId, _client.Name, _client.Description);

            return client;
        }

        public async Task<Client> Update(int id, JsonPatchDocument clientDocument)
        {
            var dbClient = await _repository.Update(id, clientDocument);
            var client = new Client(dbClient.ClientId, dbClient.Name, dbClient.Description);

            return client;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        #endregion Public Methods
    }
}