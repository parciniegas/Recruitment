namespace Sequences.Services.Subjets
{
    public class Subject
    {
        public Subject(string name, string? prefix, string? suffix, string rule, int startAt, int endAt, int value, string sequence, string? description)
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
}