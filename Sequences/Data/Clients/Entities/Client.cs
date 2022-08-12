using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sequences.Data.Clients.Entities
{
    public class Client
    {
        #region Constuctors

        public Client(int id, string name, string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        #endregion Constuctors

        #region Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(4000)]
        public string? Description { get; set; }

        #endregion Properties
    }
}