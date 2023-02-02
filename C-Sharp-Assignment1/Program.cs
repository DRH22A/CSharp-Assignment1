using Library.Canvas;
using System;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int menu = 0;
            List<Course> course_list = new List<Course>();
            List<Student> student_list = new List<Student>();

            while (menu == 0)
            {
                string user_input;
                Console.WriteLine("Please choose what you would like to do from the below options.");
                Console.WriteLine("1. Create a course and add it to a list of courses.");
                Console.WriteLine("2. Create a student and add it to a list of students.");
                Console.WriteLine("3. Add a student from the list of students to a specific course.");
                Console.WriteLine("4. Remove a student from a course's roster.");
                Console.WriteLine("5. List all courses.");
                Console.WriteLine("6. Search for a course by name or description.");
                Console.WriteLine("7. List all students.");
                Console.WriteLine("8. Search for a student by name.");
                Console.WriteLine("9. List all courses a student is taking.");
                Console.WriteLine("10. Update a course’s information.");
                Console.WriteLine("11. Update a student’s information.");
                Console.WriteLine("12. Create an assignment and add it to the list of assignments for a course.");
                Console.WriteLine("13. Exit.");
                user_input = Console.ReadLine() ?? string.Empty;
                int x = Int32.Parse(user_input);
                List<string> students_in_course_list = new List<string>();

                if (x == 1)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter the name of your new course:");
                    string tmp_course_name = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Please enter the code of the course:");
                    string tmp_course_id = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Please enter the max students for the course:");
                    string tmp_max_students = Console.ReadLine() ?? string.Empty;
                    int max_students_conversion = Int32.Parse(tmp_max_students ?? string.Empty);
                    Console.WriteLine("Please enter a description for the course:");
                    string tmp_course_description = Console.ReadLine() ?? string.Empty;
                    Course new_course = new Course(tmp_course_name, tmp_course_id, max_students_conversion, tmp_course_description);
                    Console.WriteLine("Course Name: " + new_course.course_name);
                    Console.WriteLine("Course Id: " + new_course.course_id);
                    Console.WriteLine("Current Students: " + new_course.current_students);
                    Console.WriteLine("Max Students: " + new_course.max_students);
                    Console.WriteLine("Course Description: " + new_course.course_description);
                    string new_course_str = new_course.course_name.ToString();
                    course_list.Add(new_course);
                    Console.WriteLine();
                }

                else if (x == 2)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter the name of the student: ");
                    string tmp_student_name = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Please enter the id of the student: ");
                    string tmp_student_id = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Please enter the classifcation of the student (freshman, sophomore, etc): ");
                    string tmp_student_classifcation = Console.ReadLine() ?? string.Empty;
                    Student new_student = new Student(tmp_student_name, tmp_student_id, tmp_student_classifcation);
                    Console.WriteLine("Student Name: " + tmp_student_name);
                    Console.WriteLine("Student Id: " + tmp_student_id);
                    Console.WriteLine("Student Classification: " + tmp_student_classifcation);
                    string new_student_str = new_student.student_name.ToString();
                    student_list.Add(new_student);
                    Console.WriteLine();
                }

                else if (x == 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("What course would you like to add a student to: ");
                    string course_addition = Console.ReadLine() ?? string.Empty;
                    Course finding_course = course_list.Find(c => c.course_name == course_addition);
                    Course course_found = null;
                    foreach (Course course in course_list)
                    {
                        if (course_list[course_list.IndexOf(finding_course)].course_name.Contains(course.course_name))
                        {
                            course_found = course;
                            break;
                        }
                    }
                    if (course_found != null)
                    {
                        Console.WriteLine("What is the name of the student you would like to add to " + course_addition);
                        string temp_student_name = Console.ReadLine() ?? string.Empty;
                        Student finding_student = student_list.Find(s => s.student_name == temp_student_name);
                        Student student_found = null;
                        foreach (Student student in student_list)
                        {
                            if (student_list[student_list.IndexOf(finding_student)].student_name.Contains(student.student_name))
                            {
                                student_found = student;
                                break;
                            }
                        }
                        if (student_found != null)
                        {
                            Student temp_student = student_list[student_list.IndexOf(finding_student)];
                            Course temp_student_courses = course_list[course_list.IndexOf(finding_course)];
                            Console.WriteLine("Adding \'" + temp_student_name + "\' to course -> " + course_addition + ".");
                            course_list[course_list.IndexOf(finding_course)].course_roster.Add(temp_student);
                            course_list[course_list.IndexOf(finding_course)].current_students += 1;
                            student_list[student_list.IndexOf(finding_student)].student_courses.Add(temp_student_courses);
                            Console.WriteLine("Students in \'" + course_addition + "\' are as follows: ");
                            int z = 0;
                            for (int k = 0; k < course_list[course_list.IndexOf(finding_course)].course_roster.Count; k++)
                            {
                                Console.WriteLine(++z + ". " + course_list[course_list.IndexOf(finding_course)].
                                course_roster[k].student_name);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sorry that course does not exist.");
                    }
                    Console.WriteLine();
                }

                else if (x == 4)
                {
                    Console.WriteLine();
                    Console.WriteLine("What course would you like to remove a student from: ");
                    string course_removal = Console.ReadLine() ?? string.Empty;
                    Course course_found = course_list.Find(c => c.course_name == course_removal);
                    if (course_found != null)
                    {
                        Console.WriteLine("What is the name of the student you would like to remove from " + course_removal);
                        string temp_student_name = Console.ReadLine() ?? string.Empty;
                        Student student_found = student_list.Find(s => s.student_name == temp_student_name);
                        if (student_found != null)
                        {
                            Console.WriteLine("Removing \'" + temp_student_name + "\' from course -> " + course_removal + ".");
                            course_found.course_roster.RemoveAll(s => s.student_name == temp_student_name);
                            course_list[course_list.IndexOf(course_found)].current_students -= 1;
                            Console.WriteLine("Students remaining in \'" + course_found.course_name + "\' are as follows: ");
                            int z = 0;
                            for (int k = 0; k < course_found.course_roster.Count; k++)
                            {
                                Console.WriteLine(++z + ". " + course_found.course_roster[k].student_name);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry that student does not exist in this course.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sorry that course does not exist.");
                    }
                    Console.WriteLine();

                }

                else if (x == 5)
                {
                    Console.WriteLine();
                    Console.WriteLine("Here is the list of courses.");
                    int i = 0;
                    foreach (Course course_names in course_list)
                    {
                        Console.WriteLine(++i + ". " + course_names.course_name + ", course id: " + course_names.course_id);
                    }
                    Console.WriteLine();
                }

                else if (x == 6)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please type the course/description you would like to search for: ");
                    string search_term = Console.ReadLine() ?? string.Empty;
                    Course course_found = null;
                    foreach (Course course in course_list)
                    {
                        if (course.course_name.Contains(search_term) || course.course_description.Contains(search_term))
                        {
                            course_found = course;
                        }
                    }
                    if (course_found != null)
                    {
                        Console.WriteLine("Course Name: " + course_found.course_name);
                        Console.WriteLine("Course Id: " + course_found.course_id);
                        Console.WriteLine("Current Students: " + course_found.current_students);
                        Console.WriteLine("Max Students: " + course_found.max_students);
                        Console.WriteLine("Course Description: " + course_found.course_description);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Course not found.");
                    }
                    Console.WriteLine();
                }

                else if (x == 7)
                {
                    Console.WriteLine();
                    Console.WriteLine("Here is the list of students.");
                    int i = 0;
                    foreach (Student student_names in student_list)
                    {
                        Console.WriteLine(++i + ". " + student_names.student_name + ", student id: " + student_names.student_id
                            + ", student classification: " + student_names.student_classification);
                    }
                    Console.WriteLine();
                }

                else if (x == 8)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please type the student name you would like to search for: ");
                    string search_term = Console.ReadLine() ?? string.Empty;
                    Student student_found = null;
                    foreach (Student student in student_list)
                    {
                        if (student.student_name.Contains(search_term))
                        {
                            student_found = student;
                        }
                    }
                    if (student_found != null)
                    {
                        Console.WriteLine("Student Name: " + student_found.student_name);
                        Console.WriteLine("Student Id: " + student_found.student_id);
                        Console.WriteLine("Student Classification: " + student_found.student_classification);

                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                    Console.WriteLine();
                }

                else if (x == 9)
                {
                    Console.WriteLine();
                    Console.WriteLine("What is the name of the student you would like list courses for: ");
                    string temp_student_name = Console.ReadLine() ?? string.Empty;
                    Student student_found = student_list.Find(s => s.student_name == temp_student_name);
                    if (student_found != null)
                    {
                        if (student_found.student_courses.Count > 0)
                        {
                            Console.WriteLine("Courses for \'" + temp_student_name + "\' are as follows: ");
                            int z = 0;
                            for (int k = 0; k < student_found.student_courses.Count; k++)
                            {
                                Console.WriteLine(++z + ". " + student_found.student_courses[k].course_name);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\'" + temp_student_name + "\' is not currently enrolled in any courses.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No student with the name \'" + temp_student_name + "\' was found.");
                    }
                    Console.WriteLine();
                }

                else if (x == 10)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter the name of the course you are trying to update: ");
                    string temp_course = Console.ReadLine() ?? string.Empty;
                    Course course_updating = course_list.Find(c => c.course_name == temp_course);
                    Console.WriteLine("What would you like to update [1-5]: ");
                    Console.WriteLine("1. Name");
                    Console.WriteLine("2. Id");
                    Console.WriteLine("3. Current Capacity");
                    Console.WriteLine("4. Max Capacity");
                    Console.WriteLine("5. Description");
                    string temp_string_choice = Console.ReadLine() ?? string.Empty;
                    int temp_int_choice = Int32.Parse(temp_string_choice);
                    Course course_found = null;
                    foreach (Course course in course_list)
                    {
                        if (course.course_name.Contains(temp_course))
                        {
                            course_found = course;
                        }
                    }
                    Console.WriteLine();
                    if (course_found != null)
                    {
                        if (temp_int_choice == 1)
                        {
                            Console.WriteLine("Please enter a new course name: ");
                            string new_course_name = Console.ReadLine() ?? string.Empty;
                            course_list[course_list.IndexOf(course_updating)].course_name = new_course_name;
                        }
                        if (temp_int_choice == 2)
                        {
                            Console.WriteLine("Please enter a new course id: ");
                            string new_course_id = Console.ReadLine() ?? string.Empty;
                            course_list[course_list.IndexOf(course_updating)].course_id = new_course_id;
                        }
                        if (temp_int_choice == 3)
                        {
                            Console.WriteLine("Current students in course: " + course_found.current_students);
                            Console.WriteLine("Please enter a new amount of students in the course: ");
                            string new_course_current_students = Console.ReadLine() ?? string.Empty;
                            int course_current_students_int = Int32.Parse(new_course_current_students);
                            course_list[course_list.IndexOf(course_updating)].current_students = course_current_students_int;
                        }
                        if (temp_int_choice == 4)
                        {
                            Console.WriteLine("Current max capacity in course: " + course_found.max_students);
                            Console.WriteLine("Please enter a new course max capacity: ");
                            string new_course_max = Console.ReadLine() ?? string.Empty;
                            int course_max_int = Int32.Parse(new_course_max);
                            course_list[course_list.IndexOf(course_updating)].max_students = course_max_int;
                        }
                        if (temp_int_choice == 5)
                        {
                            Console.WriteLine("Please enter a new course description: ");
                            string new_course_description = Console.ReadLine() ?? string.Empty;
                            course_list[course_list.IndexOf(course_updating)].course_description = new_course_description;
                        }
                        Console.WriteLine("Course Name: " + course_found.course_name);
                        Console.WriteLine("Course Id: " + course_found.course_id);
                        Console.WriteLine("Current Students: " + course_found.current_students);
                        Console.WriteLine("Max Students: " + course_found.max_students);
                        Console.WriteLine("Course Description: " + course_found.course_description);
                    }
                    else
                    {
                        Console.WriteLine("Course not found.");
                    }
                    Console.WriteLine();
                }

                else if (x == 11)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter the name of the student you are trying to update: ");
                    string temp_student = Console.ReadLine() ?? string.Empty;
                    Student student_updating = student_list.Find(s => s.student_name == temp_student);
                    Console.WriteLine("What would you like to update [1-3]: ");
                    Console.WriteLine("1. Name");
                    Console.WriteLine("2. Id");
                    Console.WriteLine("3. Classification");
                    string temp_string_choice = Console.ReadLine() ?? string.Empty;
                    int temp_int_choice = Int32.Parse(temp_string_choice);
                    Student student_found = null;
                    foreach (Student student in student_list)
                    {
                        if (student.student_name.Contains(temp_student))
                        {
                            student_found = student;
                        }
                    }
                    if (student_found != null)
                    {
                        if (temp_int_choice == 1)
                        {
                            Console.WriteLine("Please enter a new student name: ");
                            string new_student_name = Console.ReadLine() ?? string.Empty;
                            student_list[student_list.IndexOf(student_updating)].student_name = new_student_name;
                        }
                        else if (temp_int_choice == 2)
                        {
                            Console.WriteLine("Please enter a new student id: ");
                            string new_student_id = Console.ReadLine() ?? string.Empty;
                            student_list[student_list.IndexOf(student_updating)].student_id = new_student_id;
                        }
                        else if (temp_int_choice == 3)
                        {
                            Console.WriteLine("Please enter a new student classification: ");
                            string new_student_classification = Console.ReadLine() ?? string.Empty;
                            student_list[student_list.IndexOf(student_updating)].student_classification = new_student_classification;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                    Console.WriteLine("Student Name: " + student_found.student_name);
                    Console.WriteLine("Student Id: " + student_found.student_id);
                    Console.WriteLine("Student Classification: " + student_found.student_classification);
                    Console.WriteLine();
                }

                else if (x == 12)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter the name of the assignment you want to create: ");
                    string temp_assignment_name = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Please enter the assignment description: ");
                    string temp_assignment_description = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Please enter the points the assignment is worth: ");
                    string temp_assignment_points = Console.ReadLine() ?? string.Empty;
                    int temp_int_assignment_points= Int32.Parse(temp_assignment_points);
                    Console.WriteLine("Please enter the due date year: ");
                    string temp_assignment_date_year = Console.ReadLine() ?? string.Empty;
                    int temp_int_choice_year = Int32.Parse(temp_assignment_date_year);
                    Console.WriteLine("Please enter the due date month: ");
                    string temp_assignment_date_month = Console.ReadLine() ?? string.Empty;
                    int temp_int_choice_month = Int32.Parse(temp_assignment_date_month);
                    Console.WriteLine("Please enter the due date day: ");
                    string temp_assignment_date_day = Console.ReadLine() ?? string.Empty;
                    int temp_int_choice_day = Int32.Parse(temp_assignment_date_day);
                    Console.WriteLine("Please enter the hour the assignment is due (USE MILITARY TIME): ");
                    string temp_assignment_date_hour = Console.ReadLine() ?? string.Empty;
                    int temp_int_choice_hour = Int32.Parse(temp_assignment_date_hour);
                    Console.WriteLine("Please enter the minute the assignment is due: ");
                    string temp_assignment_date_minute = Console.ReadLine() ?? string.Empty;
                    int temp_int_choice_minute = Int32.Parse(temp_assignment_date_minute);
                    Console.WriteLine("Please enter the second the assignment is due: ");
                    string temp_assignment_date_second = Console.ReadLine() ?? string.Empty;
                    int temp_int_choice_second = Int32.Parse(temp_assignment_date_second);
                    DateTime assignment_due_date = new DateTime(temp_int_choice_year, temp_int_choice_month, temp_int_choice_day, temp_int_choice_hour, temp_int_choice_minute, temp_int_choice_second);
                    Console.WriteLine(assignment_due_date);
                    Assignment new_assignment = new Assignment(temp_assignment_name, temp_assignment_description, assignment_due_date, temp_int_assignment_points);
                    Console.WriteLine("Assignment Name: " + new_assignment.assignment_name);
                    Console.WriteLine("Assignment Id: " + new_assignment.assignment_description);
                    Console.WriteLine("Assignment Points: " + new_assignment.assignment_points);
                    Console.WriteLine("Due at: " + new_assignment.due_date);
                    Console.WriteLine("What course would you like to add this assignment to: ");
                    string course_assignment = Console.ReadLine() ?? string.Empty;
                    Course adding_assignment_to_course = course_list.Find(c => c.course_name == course_assignment);
                    Course course_found = null;
                    foreach (Course course in course_list)
                    {
                        if (course_list[course_list.IndexOf(adding_assignment_to_course)].course_name.Contains(course_assignment))
                        {
                            course_found = course;
                        }
                    }
                    if (course_found != null)
                    {
                        Console.WriteLine("Adding assignment \'" + temp_assignment_name + "\' to course -> " + course_assignment + ".");
                        course_list[course_list.IndexOf(adding_assignment_to_course)].course_assignments.Add(new_assignment);
                        Console.WriteLine("Assignments for " + course_assignment + " include: ");
                        int j = 0;
                        for (int i = 0; i < course_list[course_list.IndexOf(adding_assignment_to_course)].course_assignments.Count; i++)
                        {
                            Console.WriteLine("Assignment " + ++j + ". " + course_list[course_list.IndexOf(adding_assignment_to_course)].
                            course_assignments[i].assignment_name + " (X/" + course_list[course_list.IndexOf(adding_assignment_to_course)].
                            course_assignments[i].assignment_points + ").");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sorry that course does not exist.");
                    }
                    Console.WriteLine();
                }

                else if (x == 13)
                {
                    return;
                }
            }
        }
    }
}