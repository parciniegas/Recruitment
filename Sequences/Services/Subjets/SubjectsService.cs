using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Sequences.Data.Subjects;
using Sequences.Services.Maps;
using Sequences.Services.Subjets.Rules;

namespace Sequences.Services.Subjets
{
    public class SubjectsService : ISubjectsService
    {
        private readonly ILogger<SubjectsService> _logger;
        private readonly ISubjectRepository _subjectRepository;
        private readonly Mapper _mapper;

        public SubjectsService(ISubjectRepository subjectRepository, ILogger<SubjectsService> logger)
        {
            _subjectRepository = subjectRepository;
            _logger = logger;
            _mapper = new Mapper();
        }

        public async Task<Subject> Add(Subject subject)
        {
            var dbSubject = _mapper.GetDbSubject(subject);
            dbSubject = await _subjectRepository.Add(dbSubject);
            subject = _mapper.GetSubject(dbSubject);

            return subject;
        }

        public async Task Delete(int id)
        {
            await _subjectRepository.Delete(id);
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            var dbSubject = await _subjectRepository.GetById(id);
            var subject = _mapper.GetSubject(dbSubject);

            return subject;
        }

        public async Task<List<Subject>> GetSubjects()
        {
            var dbSubjects = await _subjectRepository.GetAll();
            var subjects = _mapper.GetSubjects(dbSubjects);

            return subjects;
        }

        public async Task<Subject> Update(Subject subject)
        {
            var dbSubject = _mapper.GetDbSubject(subject);
            dbSubject = await _subjectRepository.Update(dbSubject);
            subject = _mapper.GetSubject(dbSubject);

            return subject;
        }

        public async Task<Subject> Update(int id, JsonPatchDocument subjectDocument)
        {
            var dbSubject = await _subjectRepository.Update(id, subjectDocument);
            var subject = _mapper.GetSubject(dbSubject);

            return subject;
        }

        public async Task<string> GetNextSequece(int id)
        {
            IRule rule;
            var count = 0;
            const int limit = 3;
            var subject = await GetSubjectById(id);

            while (count < limit)
            {
                try
                {
                    rule = ResolveRule(subject);
                    rule.Apply(ref subject);
                    await Update(subject);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError("Squences was changed, retry {count}, {message}", count, ex.Message);
                    count++;
                    if (count == limit)
                        throw new TimeoutException("Timeout getting next sequence");
                    subject = await GetSubjectById(id);
                }
            }

            return subject.Sequence ?? String.Empty;
        }

        private static IRule ResolveRule(Subject subject)
        {
            if (subject.Rule == null)
                throw new ArgumentNullException(nameof(subject), "The Subject.Rule can not be null");

            var addExp = new Regex(@"^[+]\d$");
            var subExp = new Regex(@"^[-]\d$");
            if (addExp.IsMatch(subject.Rule))
                return new AdditionRule();
            if (subExp.IsMatch(subject.Rule))
                return new SubtrationRule();

            throw new ArgumentException($"The Subject.Rule value <<{subject.Rule}>> is invalid.", nameof(subject));
        }
    }
}