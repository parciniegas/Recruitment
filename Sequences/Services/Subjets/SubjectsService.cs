using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Sequences.Data.Subjects;
using db = Sequences.Data.Subjects.Entities;

namespace Sequences.Services.Subjets
{
    public class SubjectsService : ISubjectsService
    {
        private readonly ILogger<SubjectsService> _logger;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectsService(ISubjectRepository subjectRepository, ILogger<SubjectsService> logger, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Subject> Add(Subject subject)
        {
            var dbSubject = _mapper.Map<db.Subject>(subject);
            dbSubject = await _subjectRepository.Add(dbSubject);
            subject = _mapper.Map<Subject>(dbSubject);

            return subject;
        }

        public async Task Delete(int id)
        {
            await _subjectRepository.Delete(id);
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            var dbSubject = await _subjectRepository.GetById(id);
            var subject = _mapper.Map<Subject>(dbSubject);

            return subject;
        }

        public async Task<List<Subject>> GetSubjects()
        {
            var dbSubjects = await _subjectRepository.GetAll();
            var subjects = _mapper.Map<List<Subject>>(dbSubjects);

            return subjects;
        }

        public async Task<Subject> Update(Subject subject)
        {
            var dbSubject = _mapper.Map<db.Subject>(subject);
            dbSubject = await _subjectRepository.Update(dbSubject);
            subject = _mapper.Map<Subject>(dbSubject);

            return subject;
        }

        public async Task<Subject> Update(int id, JsonPatchDocument subjectDocument)
        {
            var dbSubject = await _subjectRepository.Update(id, subjectDocument);
            var subject = _mapper.Map<Subject>(dbSubject);

            return subject;
        }
    }
}