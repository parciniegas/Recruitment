using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Polly;
using Sequences.Data.Subjects;
using Sequences.Services.Maps;
using Sequences.Services.Subjects.Rules;

namespace Sequences.Services.Subjects
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

        public async Task<string> NextSequece(int id)
        {
            var policy = Policy.Handle<DbUpdateConcurrencyException>()
                .WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: sleep => TimeSpan.FromMilliseconds(500));

            var sequence = await policy.ExecuteAsync<string>(async () =>
            {
                var dbSubject = await _subjectRepository.GetById(id);
                var subject = _mapper.GetSubject(dbSubject);
                var rule = ResolveRule(subject.Rule ?? string.Empty);
                rule.Apply(ref subject);
                var sequence = subject.Sequence ?? string.Empty;
                (dbSubject.Value, dbSubject.Sequence, dbSubject.LastUpdate) = (subject.Value, subject.Sequence, DateTime.UtcNow);
                await _subjectRepository.Update(id, subject.Value, sequence);
                return sequence;
            });

            return sequence;
        }

        private static IRule ResolveRule(string rule)
        {
            var addExp = new Regex(@"^[+]\d$");
            var subExp = new Regex(@"^[-]\d$");
            if (addExp.IsMatch(rule))
                return new AdditionRule();
            if (subExp.IsMatch(rule))
                return new SubtrationRule();

            throw new ArgumentException($"The Subject.Rule value: {rule} is invalid.", nameof(rule));
        }
    }
}