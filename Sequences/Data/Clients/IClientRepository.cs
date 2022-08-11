using Sequences.Data.Clients.Entities;

namespace Sequences.Data.Clients
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAll();

        Client GetById(int id);

        Client Add(Client client);

        Client Update(Client client);

        void Delete(int id);
    }
}