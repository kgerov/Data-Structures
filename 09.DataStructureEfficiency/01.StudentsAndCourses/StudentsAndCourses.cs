using System;
using System.Collections.Generic;
using System.IO;
using _01.StudentsAndCourses.Helpers;

namespace _01.StudentsAndCourses
{
    class StudentsAndCourses
    {
        static void Main()
        {
            var studentsByCourse = new SortedDictionary<string, SortedSet<Student>>();

            using (StreamReader streamReader = new StreamReader("../../InputFile/Students.txt"))
            {
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] tokens = line.Split('|');
                    string firstName = tokens[0].Trim();
                    string lastName = tokens[1].Trim();
                    string course = tokens[2].Trim();

                    Student currentStudent = new Student(firstName, lastName, course);

                    if (!studentsByCourse.ContainsKey(course))
                    {
                        studentsByCourse.Add(course, new SortedSet<Student>());
                    }

                    studentsByCourse[course].Add(currentStudent);
                }
            }

            foreach (var course in studentsByCourse)
            {
                Console.WriteLine(course.Key + ": " + String.Join(", ", course.Value));
            }
        }
    }
}