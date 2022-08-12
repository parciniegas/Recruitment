using Microsoft.AspNetCore.JsonPatch;

namespace Sequences.Services.Clients
{
    public interface IClientsService
    {
        List<Client> GetClients();

        Client GetClientById(int id);

        Client Add(Client client);

        Client Update(Client client);

        Client Update(int id, JsonPatchDocument clientDocument);

        void Delete(int id);
    }
}