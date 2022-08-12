using Microsoft.AspNetCore.JsonPatch;
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

        public List<Client> GetClients()
        {
            var clients = _repository.GetAll()
                .Select(c => new Client(c.Id, c.Name, c.Description))
                .ToList();

            return clients;
        }

        public Client GetClientById(int id)
        {
            var client = _repository.GetById(id);

            return new Client(client.Id, client.Name, client.Description);
        }

        public Client Add(Client client)
        {
            var dbClient = new Data.Clients.Entities.Client(client.Id, client.Name, client.Description);
            var _client = _repository.Add(dbClient);
            (client.Id, client.Name, client.Description) = (_client.Id, _client.Name, _client.Description);

            return client;
        }

        public Client Update(Client client)
        {
            var dbClient = new Data.Clients.Entities.Client(client.Id, client.Name, client.Description);
            var _client = _repository.Update(dbClient);
            (client.Id, client.Name, client.Description) = (_client.Id, _client.Name, _client.Description);

            return client;
        }

        public Client Update(int id, JsonPatchDocument clientDocument)
        {
            var dbClient = _repository.Update(id, clientDocument);
            var client = new Client(dbClient.Id, dbClient.Name, dbClient.Description);

            return client;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        #endregion Public Methods
    }
}