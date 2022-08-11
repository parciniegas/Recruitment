namespace Sequences.Data.Clients.Entities
{
    public class Client
    {
        #region Constuctors

        public Client(int id, string name, string? desciption)
        {
            Id = id;
            Name = name;
            Description = desciption;
        }

        #endregion Constuctors

        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        #endregion Properties
    }
}