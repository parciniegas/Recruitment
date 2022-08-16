namespace Sequences.Api.V1.Model
{
    public class RequestSubject
    {
        public RequestSubject(string name, string? prefix, string? suffix, string rule, int startAt, int endAt, int value, string sequence, string? description)
        {
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

        public string Name { get; set; }
        public string? Prefix { get; set; }
        public string? Suffix { get; set; }
        public string Rule { get; set; }
        public int StartAt { get; set; }
        public int EndAt { get; set; }
        public int Value { get; set; }
        public string Sequence { get; set; }
        public string? Description { get; set; }
    }

    public class ResponseSubject : RequestSubject
    {
        public ResponseSubject(int id, string name, string? prefix, string? suffix, string rule, int startAt, int endAt, int value, string sequence, string? description)
            : base(name, prefix, suffix, rule, startAt, endAt, value, sequence, description)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}