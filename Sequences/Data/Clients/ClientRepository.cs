using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Sequences.Data.Clients.Entities;
using Sequences.Data.Exceptions;

namespace Sequences.Data.Clients
{
    public class ClientRepository : IClientRepository
    {
        private const string KeyDuplicated = "23505";

        private readonly Context _context;

        public ClientRepository(Context context)
        {
            _context = context;
        }

        public async Task<Client> Add(Client client)
        {
            try
            {
                await _context.AddAsync(client);
                await _context.SaveChangesAsync();

                return client;
            }
            catch (DbUpdateException ex) when (ex.InnerException != null && ex.InnerException.GetType() == typeof(PostgresException))
            {
                if (((PostgresException)(ex.InnerException)).SqlState == KeyDuplicated)
                    throw new EntityAlreadyExistException($"A client with name <<{client.Name}>> already exist.");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                throw new EntityNotFoundException($"Client with id {id} not found");
            }
            _context.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Client>> GetAll()
        {
            var clients = await _context.Clients.AsNoTracking().ToListAsync();

            return clients;
        }

        public async Task<Client> GetById(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                throw new EntityNotFoundException($"Client with id {id} not found");
            }

            return client;
        }

        public async Task<Client> Update(Client client)
        {
            var curClient = await _context.Clients.FindAsync(client.ClientId);
            if (curClient == null)
            {
                throw new EntityNotFoundException($"Client with id {client.ClientId} not found");
            }
            curClient.Name = client.Name;
            curClient.Description = client.Description;
            await _context.SaveChangesAsync();

            return curClient;
        }

        public async Task<Client> Update(int id, JsonPatchDocument client)
        {
            var curClient = await _context.Clients.FindAsync(id);
            if (curClient == null)
            {
                throw new EntityNotFoundException($"Client with id {id} not found");
            }
            client.ApplyTo(curClient);
            await _context.SaveChangesAsync();

            return curClient;
        }
    }
}