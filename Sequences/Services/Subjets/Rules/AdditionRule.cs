namespace Sequences.Services.Subjets.Rules
{
    public class AdditionRule : IRule
    {
        public void Apply(ref Subject subject)
        {
            var step = subject.Rule != null ? int.Parse(subject.Rule[1..]) : 1;

            subject.Value += step;
            subject.Sequence = $"{subject.Prefix ?? String.Empty}{subject.Value}{subject.Suffix ?? String.Empty}";
        }
    }
}