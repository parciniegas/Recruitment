using Microsoft.AspNetCore.JsonPatch;
using Sequences.Data.Clients.Entities;

namespace Sequences.Data.Clients
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAll();

        Task<Client> GetById(int id);

        Task<Client> Add(Client client);

        Task<Client> Update(Client client);

        Task<Client> Update(int id, JsonPatchDocument client);

        Task Delete(int id);
    }
}