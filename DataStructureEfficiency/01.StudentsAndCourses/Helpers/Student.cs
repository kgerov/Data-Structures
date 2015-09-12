using System;

namespace _01.StudentsAndCourses.Helpers
{
    public class Student : IComparable<Student>
    {
        public Student(string firstName, string lastName, string course)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Course = course;
        }

        public Student()
        {
            
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Course { get; set; }

        public int CompareTo(Student other)
        {
            string thisReversedName = this.LastName + this.FirstName;
            string otherReversedName = other.LastName + other.FirstName;

            return thisReversedName.CompareTo(otherReversedName);
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
