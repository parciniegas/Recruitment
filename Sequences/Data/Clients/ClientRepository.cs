using Sequences.Data.Clients.Entities;
using Sequences.Data.Exceptions;

namespace Sequences.Data.Clients
{
    public class ClientRepository : IClientRepository
    {
        private readonly Context _context;

        public ClientRepository(Context context)
        {
            _context = context;
        }

        public Client Add(Client client)
        {
            _context.Add(client);
            _context.SaveChanges();

            return client;
        }

        public void Delete(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                throw new EntityNotFoundException($"Client with id {id} not found");
            }
            _ = _context.Remove(client);
            _context.SaveChanges();
        }

        public IEnumerable<Client> GetAll()
        {
            var clients = _context.Clients;
            return clients;
        }

        public Client GetById(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                throw new EntityNotFoundException($"Client with id {id} not found");
            }

            return client;
        }

        public Client Update(Client client)
        {
            var curClient = _context.Clients.Find(client.Id);
            if (curClient == null)
            {
                throw new EntityNotFoundException($"Client with id {client.Id} not found");
            }
            curClient.Name = client.Name;
            _context.SaveChanges();

            return client;
        }
    }
}