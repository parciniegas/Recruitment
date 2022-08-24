namespace Sequences.Services.Subjects.Rules
{
    public class SubtrationRule : IRule
    {
        public void Apply(ref Subject subject)
        {
            var step = subject.Rule != null ? int.Parse(subject.Rule[1..]) : 1;

            subject.Value -= step;
            subject.Sequence = $"{subject.Prefix ?? String.Empty}{subject.Value}{subject.Suffix ?? String.Empty}";
        }
    }
}