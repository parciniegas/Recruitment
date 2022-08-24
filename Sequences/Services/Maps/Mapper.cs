using Sequences.Services.Subjects;
using db = Sequences.Data.Subjects.Entities;

namespace Sequences.Services.Maps
{
    public class Mapper
    {
        internal db.Subject GetDbSubject(Subject subject)
        {
            var dbSubject = new db.Subject(subject.SubjectId, subject.ClientId, subject.Name ?? String.Empty, subject.Prefix, subject.Suffix, subject.Rule ?? String.Empty, subject.StartAt,
                                           subject.EndAt, subject.Value, subject.Sequence, subject.Description);

            return dbSubject;
        }

        internal Subject GetSubject(db.Subject dbSubject)
        {
            var subject = new Subject(dbSubject.SubjectId, dbSubject.ClientId, dbSubject.Name, dbSubject.Prefix, dbSubject.Suffix,
                                      dbSubject.Rule, dbSubject.StartAt, dbSubject.EndAt, dbSubject.Value, dbSubject.Sequence, dbSubject.Description);

            return subject;
        }

        internal List<Subject> GetSubjects(List<db.Subject> dbSubjects)
        {
            var subjects = new List<Subject>();
            foreach (db.Subject subject in dbSubjects)
            {
                subjects.Add(GetSubject(subject));
            }

            return subjects;
        }
    }
}