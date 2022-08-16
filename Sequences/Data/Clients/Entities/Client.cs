using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Sequences.Data.Subjects.Entities;

namespace Sequences.Data.Clients.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Client
    {
        #region Constuctors

        public Client(int clientId, string name, string? description)
        {
            ClientId = clientId;
            Name = name;
            Description = description;
        }

        #endregion Constuctors

        #region Properties

        [Key]
        public int ClientId { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(4000)]
        public string? Description { get; set; }

        public ICollection<Subject>? Subjects { get; set; }

        #endregion Properties
    }
}