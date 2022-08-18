using Microsoft.AspNetCore.JsonPatch;
using Sequences.Data.Subjects.Entities;

namespace Sequences.Data.Subjects
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAll();

        Task<Subject> GetById(int id);

        Task<Subject> Add(Subject subject);

        Task<Subject> Update(Subject subject);

        Task<Subject> Update(int id, JsonPatchDocument subjectDocument);

        Task Delete(int id);

        Task<string> GetNextSequence(int subjectId);
    }
}