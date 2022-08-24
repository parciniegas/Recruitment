namespace Sequences.Services.Subjects.Rules
{
    public interface IRule
    {
        void Apply(ref Subject subject);
    }
}