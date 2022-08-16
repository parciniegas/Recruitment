using AutoMapper;
using Sequences.Api.V1.Model;
using Sequences.Services.Subjets;

namespace Sequences.Api.V1.MappingConfigurations
{
    public class SequenceProfile : Profile
    {
        public SequenceProfile()
        {
            CreateMap<RequestSubject, Subject>();
            CreateMap<Subject, ResponseSubject>();
        }
    }
}