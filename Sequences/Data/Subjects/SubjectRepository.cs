using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Sequences.Data.Exceptions;
using Sequences.Data.Subjects.Entities;

namespace Sequences.Data.Subjects
{
    public class SubjectRepository : ISubjectRepository
    {
        private const string KeyDuplicated = "23505";
        private readonly Context _context;

        public SubjectRepository(Context context)
        {
            _context = context;
        }

        public async Task<Subject> Add(Subject subject)
        {
            try
            {
                await _context.AddAsync(subject);
                await _context.SaveChangesAsync();

                return subject;
            }
            catch (DbUpdateException ex) when (ex.InnerException != null && ex.InnerException.GetType() == typeof(PostgresException))
            {
                if (((PostgresException)(ex.InnerException)).SqlState == KeyDuplicated)
                    throw new EntityAlreadyExistException($"A client with name <<{subject.Name}>> already exist.");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                throw new EntityNotFoundException($"Subject with id {id} not found");
            }
            _context.Remove(subject);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Subject>> GetAll()
        {
            var subjects = await _context.Subjects.AsNoTracking().ToListAsync();

            return subjects;
        }

        public async Task<Subject> GetById(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                throw new EntityNotFoundException($"Client with id {id} not found");
            }

            return subject;
        }

        public async Task<Subject> Update(Subject subject)
        {
            var curSubject = await _context.Subjects.FindAsync(subject.SubjectId);
            if (curSubject == null)
            {
                throw new EntityNotFoundException($"Subject with id {subject.SubjectId} not found");
            }
            curSubject.Name = subject.Name;
            curSubject.StartAt = subject.StartAt;
            curSubject.EndAt = subject.EndAt;
            curSubject.Description = subject.Description;

            await _context.SaveChangesAsync();

            return curSubject;
        }

        public async Task<Subject> Update(int id, JsonPatchDocument subjectDocument)
        {
            var curSubject = await _context.Subjects.FindAsync(id);
            if (curSubject == null)
            {
                throw new EntityNotFoundException($"Client with id {id} not found");
            }
            subjectDocument.ApplyTo(curSubject);
            await _context.SaveChangesAsync();

            return curSubject;
        }
    }
}
