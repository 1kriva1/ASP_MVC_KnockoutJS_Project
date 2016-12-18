using AutoMapper;
using Educat.Logic.Dto;
using Educat.Logic.Pages;
using Educat.Model;
using Educat.Model.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Educat.Logic
{
    public static class DataService
    {        
        /*Student methods*/
        public static void AddStudent(StudentDto dto)
        {
            using (var context = new DataContext())
            {
                Student student = Mapper.Map<StudentDto, Student>(dto);
                context.Students.Add(student);
                context.SaveChanges();
            }
        }

        public static void UpdateStudent(StudentDto dto)
        {
            using (var context = new DataContext())
            {
                var entity = context.Students.FirstOrDefault(m => m.Id == dto.Id);
                if (entity != null)
                {
                    Mapper.Map<StudentDto, Student>(dto, entity);
                    context.Students.Attach(entity);
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public static Feed<StudentDto> GetStudents(int skip, int count, string search)
        {
            var result = new Feed<StudentDto>();
            search = (search ?? string.Empty).Trim().ToLower();
            
            using (var context = new DataContext())
            {
                int total, skipReturn = 0;
                var query = context.Students.AsQueryable();
                var items = new List<StudentDto>();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(x => x.FirstName.ToLower().Contains(search)).Union(query.Where(x => x.LastName.ToLower().Contains(search)));
                    items = query.OrderByDescending(x => x.Id).Skip(skip).Take(count).ToList().Select(m => Mapper.Map<Student, StudentDto>(m)).ToList();
                    
                    total = query.OrderByDescending(x => x.Id).Skip(skip).ToList().Count;                    
                }
                else
                {
                    items = query.OrderByDescending(x => x.Id).Skip(skip).Take(count).ToList()
                        .Select(m => Mapper.Map<Student, StudentDto>(m)).ToList();
                    total = query.ToList().Count();
                }
                skipReturn = skip + items.Count();
                result.Init(total, skipReturn, items);
            }
            return result;            
        }

        /*School methods*/
        public static void AddSchool(SchoolDto dto)
        {
            using (var context = new DataContext())
            {
                School school = Mapper.Map<SchoolDto, School>(dto);
                context.Schools.Add(school);
                context.SaveChanges();
            }
        }

        public static void UpdateSchool(SchoolDto dto)
        {
            using (var context = new DataContext())
            {
                var entity = context.Schools.FirstOrDefault(m => m.Id == dto.Id);
                if (entity != null)
                {
                    entity.Name = dto.Name;                    
                    context.SaveChanges();
                }
            }
        }
                
        public static Feed<SchoolDto> GetSchools(int skip, int count, string search) 
        {
            var result = new Feed<SchoolDto>();
            search = (search ?? string.Empty).Trim().ToLower();
            using (var context = new DataContext())
            {
                int total, skipReturn = 0;
                var query = context.Schools.Include(x => x.Subjects);
                var items = new List<SchoolDto>();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(search));
                    items = query.OrderByDescending(x => x.Id).Skip(skip).Take(count).ToList()
                        .Select(m => Mapper.Map<School, SchoolDto>(m)).ToList();

                    total = query.OrderByDescending(x => x.Id).Skip(skip).ToList().Count; 
                }
                else
                {
                    items = query.OrderByDescending(x => x.Id).Skip(skip).Take(count).ToList()
                        .Select(m => Mapper.Map<School, SchoolDto>(m)).ToList();
                    total = query.ToList().Count();
                }
                skipReturn = skip + items.Count();
                result.Init(total, skipReturn, items);                
            }
            return result;
        }

        /*Subject methods*/
        public static void AddSubject(SubjectDto dto)
        {
            using (var context = new DataContext())
            {
                Subject subject = Mapper.Map<SubjectDto, Subject>(dto);
                context.Subjects.Add(subject);
                context.SaveChanges();
            }
        }


        public static void UpdateSubject(SubjectDto dto)
        {
            using (var context = new DataContext())
            {
                var entity = context.Subjects.FirstOrDefault(m => m.Id == dto.Id);
                if (entity != null)
                {
                    Mapper.Map<SubjectDto, Subject>(dto, entity);
                    context.Subjects.Attach(entity);
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public static Feed<SubjectPage> GetSubjects(int skip, int count, string search)
        {
            var result = new Feed<SubjectPage>();
            List<SubjectPage> courcePage = new List<SubjectPage>();
            search = (search ?? string.Empty).Trim().ToLower();

            using (var context = new DataContext())
            {
                int total, skipReturn = 0;
                var query = context.Subjects.AsQueryable();
                var items = new List<SubjectDto>();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(search));
                    items = query.OrderByDescending(x => x.Id).Skip(skip).Take(count).ToList().Select(m => Mapper.Map<Subject, SubjectDto>(m)).ToList();
                    foreach (var item in items)
                    {
                        var school = context.Schools.FirstOrDefault(m => m.Id == item.SchoolId);
                        courcePage.Add(new SubjectPage() { Id = item.Id, Name = item.Name, SchoolId = (int)item.SchoolId, SchoolName = school.Name,  });
                    }

                    total = query.OrderByDescending(x => x.Id).Skip(skip).ToList().Count;
                }
                else
                {
                    items = query.OrderByDescending(x => x.Id).Skip(skip).Take(count).ToList()
                        .Select(m => Mapper.Map<Subject, SubjectDto>(m)).ToList();
                    foreach (var item in items)
                    {
                        var school = context.Schools.FirstOrDefault(m => m.Id == item.SchoolId);
                        courcePage.Add(new SubjectPage() { Id = item.Id, Name = item.Name, SchoolId = (int)item.SchoolId, SchoolName = school.Name });
                    }
                    total = query.ToList().Count();
                }
                skipReturn = skip + items.Count();
                result.Init(total, skipReturn, courcePage);
            }
            return result;           

        }

        /*Cource methods*/
        public static void AddCource(CourceDto dto)
        {            
            using (var context = new DataContext())
            {
                Cource cource = Mapper.Map<CourceDto, Cource>(dto);
                context.Cources.Add(cource);
                context.SaveChanges();
            }
        }


        public static void UpdateCource(CourceDto dto)
        {
            using (var context = new DataContext())
            {
                var entity = context.Cources.FirstOrDefault(m => m.Id == dto.Id);
                if (entity != null)
                {
                    Mapper.Map<CourceDto, Cource>(dto, entity);
                    context.Cources.Attach(entity);
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public static Feed<CourcePage> GetCources(int skip, int count, string search)
        {
            var result = new Feed<CourcePage>();
            List<CourcePage> courcePage = new List<CourcePage>();
            search = (search ?? string.Empty).Trim().ToLower();

            using (var context = new DataContext())
            {
                int total, skipReturn = 0;
                var query = context.Cources.AsQueryable();
                var items = new List<Cource>();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(x => x.SubjectName.ToLower().Contains(search));
                    items = query.OrderByDescending(x => x.Id).Skip(skip).Take(count).ToList().ToList();
                    foreach (var item in items)
                    {
                        var school = context.Schools.FirstOrDefault(m => m.Id == item.SchoolId);
                        courcePage.Add(new CourcePage()
                        {
                            Id = item.Id,
                            SchoolId = school.Id,
                            SubjectName = item.SubjectName,
                            SubjectId = item.SubjectId,
                            Students = item.Students,
                            StartDateString = item.StartDate.ToString("d/M/yyyy"),
                            Grades = null,
                            FinishDateString = item.FinishDate.ToString("d/M/yyyy")
                        });
                    }

                    total = query.OrderByDescending(x => x.Id).Skip(skip).ToList().Count;
                }
                else
                {
                    items = query.OrderByDescending(x => x.Id).Skip(skip).Take(count).ToList()
                        .ToList();
                    foreach (var item in items)
                    {
                        var school = context.Schools.FirstOrDefault(m => m.Id == item.SchoolId);
                        courcePage.Add(new CourcePage()
                        {
                            Id = item.Id,
                            SchoolId = school.Id,
                            SubjectName = item.SubjectName,
                            SubjectId = item.SubjectId,
                            Students = item.Students,
                            StartDateString = item.StartDate.ToString("d/M/yyyy"),
                            Grades = null,
                            FinishDateString = item.FinishDate.ToString("d/M/yyyy")
                        });
                    }
                    total = query.ToList().Count();
                }
                skipReturn = skip + items.Count();
                result.Init(total, skipReturn, courcePage);
            }
            return result;

        }
                
        public static List<StudentDto> GetAllStudents(int schoolId)
        {
            List<StudentDto> allStudents = new List<StudentDto>();
            using (var context = new DataContext())
            {
                List<Student> entity = context.Students.Where(m => m.Schools.Any(n => n.Id == schoolId)).ToList();
                Mapper.Map<List<Student>, List<StudentDto>>(entity, allStudents);  
            }
            return allStudents;
        }

        public static List<SubjectDto> GetAllSubjects(int schoolId)
        {
            using (var context = new DataContext())
            {
                return context.Subjects.Where(m => m.SchoolId == schoolId).ToList()
                    .Select(m => Mapper.Map<Subject, SubjectDto>(m)).ToList();                
            }
        }

        public static void DeleteStudent(int studentId, int schoolId)
        {
            using (var context = new DataContext())
            {
                var school = context.Schools.Find(schoolId);
                var student = context.Students.Find(studentId);
                var cource = context.Cources.ToList();
                context.Entry(school).Collection("Students").Load();                
                school.Students.Remove(student);
                
                foreach (var item in cource)
                {
                    context.Entry(item).Collection("Students").Load();
                    Student temp = item.Students.FirstOrDefault(m => m.Id == studentId);
                    if (temp !=null)
                    {
                        item.Students.Remove(item.Students.FirstOrDefault(m => m.Id == studentId));
                    }                    
                }
                context.SaveChanges();
            }
        }

        public static void DeleteSubject(int subjectId, int schoolId)
        {
            using (var context = new DataContext())
            {
                var school = context.Schools.Find(schoolId);
                var subject = context.Subjects.Find(subjectId);
                school.Subjects.Remove(subject);
                context.Subjects.Remove(subject);
                var cources = context.Cources.Where(m => m.SubjectId == subjectId).ToList();
                foreach (var item in cources)
                {
                    context.Cources.Remove(item);
                }
                context.SaveChanges();                
            }            
        }

        public static List<StudentDto> GetAvailableStudents(int schoolId)
        {
            using (var context = new DataContext())
            {
                return context.Students.Where(m =>! m.Schools.Any(n => n.Id == schoolId)).ToList().Select(m => Mapper.Map<Student, StudentDto>(m)).ToList();                
            }
        }

        public static void AddStudentToSchool(int studentId, int schoolId) 
        {
            using (var context = new DataContext())
            {
                var school = context.Schools.FirstOrDefault(m => m.Id == schoolId);
                var student = context.Students.Find(studentId);
                school.Students.Add(student);
                context.SaveChanges();
            }
        }

        public static void AddSubjectToSchool(string subjectName, int schoolId)
        {
            using (var context = new DataContext())
            {
                var entity = context.Subjects.FirstOrDefault(m => m.Name == subjectName);
                var school = context.Schools.Find(schoolId);
                if (entity == null)
                {
                    school.Subjects.Add(new Subject() { Name = subjectName, SchoolId = schoolId });
                }                
                context.SaveChanges();
            }
        }

        public static List<StudentDto> GetStudentsOfSchool(int schoolId, int subjectId)
        {
            using (var context = new DataContext())
            {  
                return context.Students.Where(m => m.Schools.Any(n => n.Id == schoolId))
                    .Where(x =>! x.Cources.Any(y => y.SubjectId == subjectId)).ToList()
                    .Select(y => Mapper.Map<Student, StudentDto>(y)).ToList(); 
            }
        }

        public static void AddStudentToSubject(int studentId, int subjectId)
        {
            using (var context = new DataContext())
            {
                var student = context.Students.Find(studentId);
                var cource = context.Cources.Find(subjectId);
                cource.Students.Add(student);
                
                context.SaveChanges();
            }
        }

        public static void DeleteStudentFromSubject(int studentId, int subjectId)
        {
            using (var context = new DataContext())
            {
                var student = context.Students.Find(studentId);
                var cource = context.Cources.Find(subjectId);
                context.Entry(cource).Collection("Students").Load();
                cource.Students.Remove(student);
                
                context.SaveChanges();
            }
        }

        public static void AddStudentToCource(int studentId, int courceId)
        {
            using (var context = new DataContext())
            {
                var student = context.Students.Find(studentId);
                var cource = context.Cources.Find(courceId);
                cource.Students.Add(student);

                context.Grades.Add(new Grade() { Mark = null, StudentId = studentId, CourceId = cource.Id,
                    Cource = cource });

                context.SaveChanges();
            }
        }

        public static void DeleteStudentFromCource(int studentId, int courceId)
        {
            using (var context = new DataContext())
            {
                var student = context.Students.Find(studentId);
                var cource = context.Cources.Find(courceId);
                var grade = context.Grades
                    .Where(m => m.CourceId == courceId)
                    .Where(z => z.StudentId == studentId)
                    .ToList();
                foreach (var item in grade)
                {
                    context.Grades.Remove(item);
                }
                context.Entry(cource).Collection("Students").Load();
                cource.Students.Remove(student);

                context.SaveChanges();
            }
        }


        public static List<StudentDto> GetCurrentStudents(int subjectId, int schoolId)
        {
            using (var context = new DataContext())
            {
                List<Student> entity = context.Students
                    .Where(m=>m.Cources.Any(n=>n.SubjectId == subjectId))
                    .Where(y=>y.Schools.Any(z=>z.Id == schoolId)).ToList();    
                
                List<StudentDto> dto = new List<StudentDto>();
                Mapper.Map<List<Student>, List<StudentDto>>(entity, dto);
                return dto;
            }
        }

        //Home methods
        public static StudentDto Student(int id)
        {
            var model = new Student();
            using (var context = new DataContext())
            {
                model = context.Students.Find(id);
                return Mapper.Map<Student, StudentDto>(model);
            }
        }

        public static School School(int id)
        {
            var model = new School();
            using (var context = new DataContext())
            {
                model = context.Schools.Find(id);
            }
            return model;
        }

        public static SubjectPage Subject(int id)
        {
            var model = new SubjectPage();
            using (var context = new DataContext())
            {
                var cource = context.Subjects.Find(id);
                var school = context.Schools.FirstOrDefault(m=>m.Id == cource.SchoolId);
                model = new SubjectPage() { Id = cource.Id, Name = cource.Name, SchoolId = (int)cource.SchoolId, SchoolName = school.Name};
            }
            return model;
        }

        public static CourcePage Cource(int id)
        {
            var model = new CourcePage();
            using (var context = new DataContext())
            {
                var cource = context.Cources.Find(id);
                var school = context.Schools.FirstOrDefault(m => m.Id == cource.SchoolId);
                var subjectName = context.Subjects.FirstOrDefault(m => m.Id == cource.SubjectId).Name;
                
                model = new CourcePage() {
                    Id = cource.Id,
                    Grades = null,
                    Students = cource.Students,
                    SubjectId = cource.SubjectId,
                    SubjectName = subjectName,
                    SchoolId = school.Id,
                    SchoolName = school.Name,
                    StartDateString = cource.StartDate.ToString("d/M/yyyy"),
                    FinishDateString = cource.FinishDate.ToString("d/M/yyyy")                
                };
            }
            return model;
        }

        public static List<SubjectDto> GetAvailableSubjects(int schoolId)
        {
            using (var context = new DataContext())
            {
                return context.Subjects.ToList().Where(m => m.SchoolId == schoolId).Select(m => Mapper.Map<Subject, SubjectDto>(m)).ToList();
            }           
        }

        public static List<StudentPage> GetStudentsForCource(int subjectId, int schoolId)
        {
            var list = new List<StudentPage>();
            using (var context = new DataContext())
            {
                List<Student> entity = context.Students
                    .Where(m => m.Cources.Any(n => n.SubjectId == subjectId))
                    .Where(y => y.Schools.Any(z => z.Id == schoolId)).ToList();

                foreach (var item in entity)
                {
                    int? mark = null;
                    List<Cource> cources = context.Cources.Include(q=>q.Students)
                        .Where(x=>x.SubjectId == subjectId)
                        .Where(x=>x.SchoolId == schoolId)
                        .Where(x=>x.Students.Any(y=>y.Id == item.Id))                        
                        .ToList();

                    var gradeTemp = context.Grades.FirstOrDefault(c => c.StudentId == item.Id);
                    if (gradeTemp != null)
                        mark = gradeTemp.Mark;

                    list.Add(new StudentPage() { 
                        Dob = item.Dob,
                        Email = item.Email,
                        FirstName = item.FirstName,
                        Id = item.Id,
                        LastName = item.LastName,
                        Mark = mark
                    });
                }
                return list;
            }
        }

        public static void ChangeMark(int studentId, int courceId, int mark)
        {
            using (var context = new DataContext())
            {
                var entity = context.Grades.Where(q => q.CourceId == courceId)
                                .Where(q => q.StudentId == studentId).ToList();
                foreach (var item in entity)
                {
                    item.Mark = mark;
                }

                context.SaveChanges();
            }
        }
    }
}
