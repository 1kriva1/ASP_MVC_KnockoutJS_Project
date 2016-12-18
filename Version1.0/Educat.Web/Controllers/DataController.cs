using Educat.Logic;
using Educat.Logic.Dto;
using Educat.Logic.Pages;
using Educat.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Educat.Web.Controllers
{
    public class DataController : Controller
    {
        private string Result(object data)
        {
            string json = JsonConvert.SerializeObject(data);
            return json;
        }

        /*Student methods*/
        [HttpPost]
        public void AddStudent(StudentDto student)
        {            
            DataService.AddStudent(student);            
        }

        [HttpPost]
        public void UpdateStudent(StudentDto student)
        {
            DataService.UpdateStudent(student);            
        }

        [HttpPost]
        public string GetStudents(int skip, string search)
        {
            return Result(DataService.GetStudents(skip, Settings.ItemsCount, search));
        }

        /*School methods*/
        [HttpPost]
        public void AddSchool(SchoolDto school)
        {
            DataService.AddSchool(school);
        }

        [HttpPost]
        public void UpdateSchool(SchoolDto school)
        {
            DataService.UpdateSchool(school);
        }

        [HttpPost]
        public string GetSchools(int skip, string search)
        {
            return Result(DataService.GetSchools(skip, Settings.ItemsCount, search));
        }

        /*Subject methods*/
        [HttpPost]
        public void AddSubject(SubjectDto subject)
        {
            DataService.AddSubject(subject);
        }

        [HttpPost]
        public void UpdateSubject(SubjectDto subject)
        {
            DataService.UpdateSubject(subject);
        }

        [HttpPost]
        public string GetSubject(int skip, string search)
        {
            return Result(DataService.GetSubjects(skip, Settings.ItemsCount, search));
        }

        /*Cource methods*/

        [HttpPost]
        public void AddCource(CourceDto cource)
        {
            DataService.AddCource(cource);
        }

        [HttpPost]
        public void UpdateCource(CourceDto cource)
        {
            DataService.UpdateCource(cource);
        }

        [HttpPost]
        public string GetCources(int skip, string search)
        {
            return Result(DataService.GetCources(skip, Settings.ItemsCount, search));
        }
       
        [HttpPost]
        public string GetAllStudents(int id)
        {
            return Result(DataService.GetAllStudents(id));
        }

        [HttpPost]
        public string GetAllSubjects(int id)
        {
            return Result(DataService.GetAllSubjects(id));
        }

        [HttpPost]
        public void DeleteStudent(int studentId, int schoolId)
        {
            DataService.DeleteStudent(studentId, schoolId);
        }

        [HttpPost]
        public void DeleteSubject(int subjectId, int schoolId)
        {
            DataService.DeleteSubject(subjectId, schoolId);
        }

        [HttpPost]
        public string GetAvailableStudents(int id)
        {
            return Result(DataService.GetAvailableStudents(id));            
        }

        [HttpPost]
        public void AddStudentToSchool(int schoolId, int studentId)
        {
            DataService.AddStudentToSchool(studentId, schoolId);            
        }

        [HttpPost]
        public void AddSubjectToSchool(int schoolId, string newSubject)
        {
            DataService.AddSubjectToSchool(newSubject, schoolId);            
        }

        [HttpPost]
        public string GetStudentsOfSchool(int schoolId, int subjectId)
        {
            return Result(DataService.GetStudentsOfSchool(schoolId, subjectId));
        }

        [HttpPost]
        public void AddStudentToSubject(int subjectId, int studentId)
        {
            DataService.AddStudentToSubject(studentId, subjectId); 
        }

        [HttpPost]
        public void DeleteStudentFromSubject(int subjectId, int studentId)
        {
            DataService.DeleteStudentFromSubject(studentId, subjectId);
        }

        [HttpPost]
        public string GetCurrentStudents(int schoolId, int subjectId)
        {
            return Result(DataService.GetCurrentStudents(subjectId, schoolId));
        }


        [HttpPost]
        public void AddStudentToCource(int courceId, int studentId)
        {
            DataService.AddStudentToCource(studentId, courceId);
        }

        [HttpPost]
        public void DeleteStudentFromCource(int courceId, int studentId)
        {
            DataService.DeleteStudentFromCource(studentId, courceId);
        }

        [HttpPost]
        public string GetAvailableSubjects(int schoolId)
        {
            return Result(DataService.GetAvailableSubjects(schoolId));
        }

        [HttpPost]
        public string GetStudentsForCource(int schoolId, int subjectId)
        {
            return Result(DataService.GetStudentsForCource(subjectId, schoolId));
        }

        [HttpPost]
        public void ChangeMark(int courceId, int studentId, int mark)
        {
            DataService.ChangeMark(studentId, courceId, mark);
        }
    }
}
