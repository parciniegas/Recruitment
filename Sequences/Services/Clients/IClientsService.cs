using Microsoft.AspNetCore.JsonPatch;

namespace Sequences.Services.Clients
{
    public interface IClientsService
    {
        Task<List<Client>> GetClients();

        Task<Client> GetClientById(int id);

        Task<Client> Add(Client client);

        Task<Client> Update(Client client);

        Task<Client> Update(int id, JsonPatchDocument clientDocument);

        Task Delete(int id);
    }
}