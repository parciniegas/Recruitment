namespace Sequences.Services.Subjets
{
    public class Subject
    {
        public Subject(int subjectId, int clientId, string? name, string? prefix, string? suffix,
                       string? rule, int startAt, int endAt, int value, string? sequence, string? description)
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

        public int SubjectId { get; set; }
        public int ClientId { get; set; }
        public string? Name { get; set; }
        public string? Prefix { get; set; }
        public string? Suffix { get; set; }
        public string? Rule { get; set; }
        public int StartAt { get; set; }
        public int EndAt { get; set; }
        public int Value { get; set; }
        public string? Sequence { get; set; }
        public string? Description { get; set; }
    }
}