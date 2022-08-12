﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Client> Add(Client client)
        {
            await _context.AddAsync(client);
            await _context.SaveChangesAsync();

            return client;
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
            var curClient = await _context.Clients.FindAsync(client.Id);
            if (curClient == null)
            {
                throw new EntityNotFoundException($"Client with id {client.Id} not found");
            }
            curClient.Name = client.Name;
            curClient.Description = client.Description;
            await _context.SaveChangesAsync();

            return client;
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