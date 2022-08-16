using System.ComponentModel.DataAnnotations;

namespace Sequences.Api.V1.Model
{
    public class BaseSubject
    {
        public BaseSubject(string name, int startAt, int endAt, string? description)
        {
            Name = name;
            StartAt = startAt;
            EndAt = endAt;
            Description = description;
        }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public int StartAt { get; set; }

        [Required]
        public int EndAt { get; set; }

        [MaxLength(4000)]
        public string? Description { get; set; }
    }

    public class UpdateSubject : BaseSubject
    {
        public UpdateSubject(int subjectId, string name, int startAt, int endAt, string? description)
            : base(name, startAt, endAt, description)
        {
            SubjectId = subjectId;
        }

        [Required]
        public int SubjectId { get; set; }
    }

    public class CreateSubject : BaseSubject
    {
        public CreateSubject(int clientId, string name, string? prefix, string? suffix, string rule, int startAt, int endAt, string? description) :
            base(name, startAt, endAt, description)
        {
            ClientId = clientId;
            Prefix = prefix;
            Suffix = suffix;
            Rule = rule;
        }

        [Required]
        public int ClientId { get; set; }

        [MaxLength(10)]
        public string? Prefix { get; set; }

        [MaxLength(10)]
        public string? Suffix { get; set; }

        [Required, MaxLength(50)]
        public string Rule { get; set; }
    }

    public class ResponseSubject : BaseSubject
    {
        public ResponseSubject(int clientId, int subjectId, string name, string? prefix, string? suffix, string rule, int startAt, int endAt, int value, string sequence, string? description)
            : base(name, startAt, endAt, description)
        {
            ClientId = clientId;
            SubjectId = subjectId;
            Prefix = prefix;
            Suffix = suffix;
            Rule = rule;
            Value = value;
            Sequence = sequence;
        }

        public int ClientId { get; set; }
        public int SubjectId { get; set; }
        public string? Prefix { get; set; }
        public string? Suffix { get; set; }
        public string Rule { get; set; }
        public int Value { get; set; }
        public string Sequence { get; set; }
    }
}