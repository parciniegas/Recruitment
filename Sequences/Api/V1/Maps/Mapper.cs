using Sequences.Api.V1.Model;
using Sequences.Services.Subjets;

namespace Sequences.Api.V1.Maps
{
    public class Mapper
    {
        internal Subject GetSubjectForCreate(CreateSubject createSubject)
        {
            var subject = new Subject(0, createSubject.ClientId, createSubject.Name, createSubject.Prefix,
                                      createSubject.Suffix, createSubject.Rule, createSubject.StartAt,
                                      createSubject.EndAt, createSubject.StartAt, null, createSubject.Description);

            return subject;
        }

        internal ResponseSubject GetResponseSubject(Subject subject)
        {
            var responseSubject =
                new ResponseSubject(subject.ClientId, subject.SubjectId, subject.Name ?? string.Empty, subject.Prefix, subject.Suffix,
                                    subject.Rule, subject.StartAt, subject.EndAt, subject.Value, subject.Sequence, subject.Description);

            return responseSubject;
        }

        internal List<ResponseSubject> GetResponseSubjects(List<Subject> subjects)
        {
            var responseSubjects = new List<ResponseSubject>();
            foreach (Subject subject in subjects)
            {
                responseSubjects.Add(GetResponseSubject(subject));
            }

            return responseSubjects;
        }

        internal Subject GetSubjectForUpdate(UpdateSubject updateSubject)
        {
            var subject = new Subject(updateSubject.SubjectId, 0, updateSubject.Name, null, null, null,
                                      updateSubject.StartAt, updateSubject.EndAt, 0, null, updateSubject.Description);

            return subject;
        }
    }
}