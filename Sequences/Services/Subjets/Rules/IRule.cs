namespace Sequences.Services.Subjets.Rules
{
    public interface IRule
    {
        void Apply(ref Subject subject);
    }
}