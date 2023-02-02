using System.Diagnostics.Metrics;

namespace Library.Canvas
{
    public class Course
    {
        public Course()
        {
            course_name = "/0";
            course_id = "/0";
            current_students = 0;
            max_students = 0;
            course_description = "/0";
            course_roster = new List<Student>();
            course_assignments = new List<Assignment>();
            course_modules = new List<Module>();
        }

        public Course(string new_course_name, string new_course_id, int new_max_students, string new_course_description)
        {
            course_name = new_course_name;
            course_id = new_course_id;
            max_students = new_max_students;
            current_students = 0;
            course_description = new_course_description;
            course_roster = new List<Student>();
            course_assignments = new List<Assignment>();
            course_modules = new List<Module>();

        }
        public string course_name { get; set; }
        public string course_id { get; set; }
        public int current_students { get; set; }
        public int max_students { get; set; }
        public string course_description { get; set; }
        public List<Student> course_roster { get; set; }
        public List<Assignment> course_assignments { get; set; }
        public List<Module> course_modules { get; set; }


    }

    public class Student
    {
        public Student()
        {
            student_name = "/0";
            student_id = "/0";
            student_classification = "/0";
            student_grades = new Dictionary<Assignment, double>();
            student_courses = new List<Course>();
        }

        public Student(string new_student_name, string new_student_id, string new_student_classification)
        {
            student_name = new_student_name;
            student_id = new_student_id;
            student_classification = new_student_classification;
            student_grades = new Dictionary<Assignment, double>();
            student_courses = new List<Course>();
        }

        public string student_name { get; set; }
        public string student_id { get; set; }
        public string student_classification { get; set; }
        public Dictionary<Assignment, double> student_grades { get; set; }
        public List<Course> student_courses { get; set; }

    }

    public class Assignment
    {
        public Assignment()
        {
            assignment_name = "/0";
            assignment_description = "/0";
            assignment_id = 0;
            due_date = default(DateTime);
            assignment_points = 0;
        }
        public Assignment(string new_assignment_name, string new_assignment_description, 
            DateTime new_due_date, int new_assignment_points)
        {
            assignment_name = new_assignment_name;
            assignment_description = new_assignment_description;
            assignment_id = 0;
            due_date = new_due_date;
            assignment_points = new_assignment_points;
        }
        public string assignment_name { get; set; }
        public string assignment_description { get; set; }
        public int assignment_points { get; set; }
        public DateTime due_date { get; set; }
        public int assignment_id { get; set; }

    }

    public class Module
    {
        public Module()
        {
            module_name = "/0";
            module_description = "/0";
            content_list = new List<ContentItem>();
        }
        public string module_name { get; set; }
        public string module_description { get; set; }
        public List<ContentItem> content_list { get; set; }
    }

    public class ContentItem
    {
        public ContentItem()
        {
            content_name = "/0";
            content_description = "/0";
            content_path = "/0";
        }
        public string content_name { get; set; }
        public string content_description { get; set; }
        public string content_path { get; set; }

    }
}