using Microsoft.AspNetCore.JsonPatch;

namespace Sequences.Services.Subjets
{
    public class SubjectsService : ISubjectsService
    {
        public Task<Subject> Add(Subject subject)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> GetSubjectById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Subject>> GetSubjects()
        {
            throw new NotImplementedException();
        }

        public Task<Subject> Update(Subject subject)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> Update(int id, JsonPatchDocument subjectDocument)
        {
            throw new NotImplementedException();
        }
    }
}