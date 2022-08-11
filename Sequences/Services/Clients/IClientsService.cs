namespace Sequences.Services.Clients
{
    public interface IClientsService
    {
        List<Client> GetClients();

        Client GetClientById(int id);

        Client Add(Client client);

        Client Update(Client client);

        void Delete(int id);
    }
}