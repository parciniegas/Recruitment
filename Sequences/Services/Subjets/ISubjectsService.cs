using Microsoft.AspNetCore.JsonPatch;

namespace Sequences.Services.Subjets
{
    public interface ISubjectsService
    {
        Task<List<Subject>> GetSubjects();

        Task<Subject> GetSubjectById(int id);

        Task<Subject> Add(Subject subject);

        Task<Subject> Update(Subject subject);

        Task<Subject> Update(int id, JsonPatchDocument subjectDocument);

        Task Delete(int id);

        Task<string> GetNextSequece(int id);
    }
}