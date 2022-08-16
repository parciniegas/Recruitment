using AutoMapper;
using Sequences.Api.V1.Model;
using dto = Sequences.Services.Subjets;
using db = Sequences.Data.Subjects.Entities;

namespace Sequences.Api.V1.MappingConfigurations
{
    public class SequenceProfile : Profile
    {
        public SequenceProfile()
        {
            CreateMap<UpdateSubject, dto.Subject>();
            CreateMap<CreateSubject, dto.Subject>();
            CreateMap<dto.Subject, ResponseSubject>();
            CreateMap<dto.Subject, db.Subject>();
        }
    }
}