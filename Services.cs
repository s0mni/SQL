using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFProject
{
    class Services
    {
        private readonly SchoolContext _context = new SchoolContext();

        public Department CreateDepartment(string depName, List<Student> students, List<Lecture> le)
        {
            // name - new department name
            // students - students to add
            // le - lectures to add

            var department = _context.Departments.FirstOrDefault(d => d.Name == depName);
            if (department == null)
            {
                department = new Department();
                department.Name = depName;
                department.Students = students;
                department.Lectures = le;
                _context.Departments.Add(department);
                _context.SaveChanges();
            }
            return department;
        }

        public void AddToDepartment(Department dep, List<Student> students, List<Lecture> le)
        {
            // dep - in which department
            // students - students to add
            // le - lectures to add

            var department = _context.Departments.Include(x => x.Students).Include(y => y.Lectures).FirstOrDefault(d => d.Name == dep.Name);
            if (department != null)
            {
                department.Students.AddRange(students);

                foreach (var lecture in le)
                {
                    var existingLe = _context.Lectures.FirstOrDefault(l => l.Name == lecture.Name);
                    if (existingLe == null)
                    {
                        department.Lectures.Add(lecture);
                    }
                }
                _context.SaveChanges();
            }
        }

        public Lecture CreateLecture(Lecture le, List<Department> dep)
        {
            // le - lectures to create
            // dep - departments to assign the lecture to

            var lecture = _context.Lectures.FirstOrDefault(l => l.Name == le.Name);
            if (lecture == null)
            {
                lecture = new Lecture();
                lecture.Name = le.Name;
                lecture.Departments = dep;
                _context.Lectures.Add(lecture);
                _context.SaveChanges();
            }
            return lecture;
        }

        public Student CreateStudent(string stuName, Department dep)
        {
            // stuName - new student name
            // dep - department to assign the student to

            var student = _context.Students.FirstOrDefault(s => s.Name == stuName);
            var department = _context.Departments.FirstOrDefault(d => d.Name == dep.Name);
            if (student == null)
            {
                student = new Student();
                student.Name = stuName;
                department.Students.Add(student);
                _context.Students.Add(student);
                _context.SaveChanges();
            }
            return student;
        }

        public void TransferStudent(Student stu, Department dep)
        {
            // stu - which student
            // dep - new department

            var student = _context.Students.FirstOrDefault(s => s.Name == stu.Name);
            var oldDep = _context.Departments.FirstOrDefault(d => d.Students.Contains(student));
            var newDep = _context.Departments.FirstOrDefault(d => d.Name == dep.Name);
            if (student != null && oldDep != null && newDep != null)
            {
                newDep.Students.Add(student);
                oldDep.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        public void ShowStudents(Department dep)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Name == dep.Name);
            var students = department.Students.Select(s => s);
            if (department != null && students != null)
            {
                foreach (var student in students)
                {
                    Console.WriteLine(student);
                }
            }
        }

        public void ShowLectures(Department dep)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Name == dep.Name);
            var lectures = _context.Lectures.Where(l => l.Departments.Contains(department));
            if (department != null && lectures != null)
            {
                foreach (var lecture in lectures)
                {
                    Console.WriteLine(lecture);
                }
            } 
        }

        public void ShowStudentLectures(string stuName)
        {
            var student = _context.Students.FirstOrDefault(s => s.Name == stuName);
            var dep = _context.Departments.FirstOrDefault(d => d.Students.Contains(student));
            var lec = _context.Lectures.Where(l => l.Departments.Contains(dep));
            if (student != null && dep != null && lec != null)
            {
                foreach (var lecture in lec)
                {
                    Console.WriteLine(lecture);
                }
            }
        }
    }
}
