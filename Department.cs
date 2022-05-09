using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject
{
    class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Lecture> Lectures { get; set; } = new List<Lecture>();

        public override string ToString()
        {
            return Name;
        }
    }
}
