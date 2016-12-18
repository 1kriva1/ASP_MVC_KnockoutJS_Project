using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Educat.Model;
using Educat.Model.Table;

namespace Educat.Logic.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestGetList()
        {
            using(DataContext modelEntity = new DataContext())
            {
                List<Student> students = modelEntity.Students.ToList();
                Assert.IsNotNull(students);
            }            
        }

        [TestMethod]
        public void TestAddData()
        {
            using (DataContext modelEntity = new DataContext())
            {                
                Student newStudent = new Student { FirstName = "Taras", LastName = "Polischuk", Dob = DateTime.Now };
                modelEntity.Students.Add(newStudent);
                modelEntity.SaveChanges();

                Assert.IsNotNull(modelEntity.Students.Where(m => m.FirstName == newStudent.FirstName).Where(m=>m.LastName == newStudent.LastName));
            }
        }

        [TestMethod]
        public void TestDeleteData()
        {
            using (DataContext modelEntity = new DataContext())
            {
                Student newStudent = new Student { FirstName = "Igor", LastName = "Polischuk", Dob = DateTime.Now };
                modelEntity.Students.Add(newStudent);
                modelEntity.SaveChanges();

                modelEntity.Students.Remove(newStudent);
                modelEntity.SaveChanges();

                IQueryable<Student> studentTest = modelEntity.Students.Where(m => m.FirstName == newStudent.FirstName).Where(m => m.LastName == newStudent.LastName);
                Student test = null;
                foreach (var item in studentTest)
                {
                    test = item;
                }
                Assert.IsNull(test);                
            }
        }
    }
}
