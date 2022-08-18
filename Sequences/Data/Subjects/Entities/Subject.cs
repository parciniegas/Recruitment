using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Sequences.Data.Clients.Entities;

namespace Sequences.Data.Subjects.Entities
{
    [Index(nameof(ClientId), nameof(Name), IsUnique = true)]
    public class Subject
    {
        public Subject(int subjectId, int clientId, string name, string? prefix, string? suffix, string rule, int startAt, int endAt, int value, string? sequence, string? description)
        {
            SubjectId = subjectId;
            ClientId = clientId;
            Name = name;
            Prefix = prefix;
            Suffix = suffix;
            Rule = rule;
            StartAt = startAt;
            EndAt = endAt;
            Value = value;
            Sequence = sequence;
            Description = description;
        }

        [Key]
        public int SubjectId { get; set; }

        public int ClientId { get; set; }
        public Client? Client { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string? Prefix { get; set; }

        [MaxLength(10)]
        public string? Suffix { get; set; }

        [Required, MaxLength(50)]
        public string Rule { get; set; }

        [Required]
        public int StartAt { get; set; }

        [Required]
        public int EndAt { get; set; }

        [Required]
        public int Value { get; set; }

        [MaxLength(50)]
        public string? Sequence { get; set; }

        [MaxLength(4000)]
        public string? Description { get; set; }

        [ConcurrencyCheck]
        public DateTime LastUpdate { get; set; }
    }
}