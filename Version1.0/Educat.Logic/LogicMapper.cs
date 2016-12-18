using AutoMapper;
using Educat.Logic.Dto;
using Educat.Model;
using Educat.Model.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Logic.MapperConfig
{
    public class LogicMapper
    {
        public static void MapConfig()
        {
            string format = "d/M/yyyy";

            Mapper.CreateMap<StudentDto, Student>().ForMember(d => d.Dob, o => o.MapFrom(s => DateTime.ParseExact(s.DobString, format, null)));
            Mapper.CreateMap<Student, StudentDto>().ForMember(d => d.DobString, o => o.MapFrom(s => s.Dob.ToString(format)));

            Mapper.CreateMap<SchoolDto, School>();
            Mapper.CreateMap<School, SchoolDto>();

            Mapper.CreateMap<SubjectDto, Subject>();
            Mapper.CreateMap<Subject, SubjectDto>();
            
            
            Mapper.CreateMap<Cource, CourceDto>().ForMember(d=>d.StartDateString, o=>o.MapFrom(s=>s.StartDate.ToString(format)))
                .ForMember(d => d.FinishDateString, o => o.MapFrom(s => s.FinishDate.ToString(format)));

            Mapper.CreateMap<CourceDto, Cource>().ForMember(d => d.StartDate, o => o.MapFrom(s => DateTime.ParseExact(s.StartDateString, format, null)))
                .ForMember(d => d.FinishDate, o => o.MapFrom(s => DateTime.ParseExact(s.FinishDateString, format, null)));

          

            Mapper.AssertConfigurationIsValid();
        }
    }
}
