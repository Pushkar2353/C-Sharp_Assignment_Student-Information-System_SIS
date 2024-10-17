using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Enrollment> Enrollments { get; set; } // List of enrollments
        public List<Payment> PaymentHistory { get; set; } // List of payments

        // Constructor
        public Student(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
            Enrollments = new List<Enrollment>(); // Initialize empty list
            PaymentHistory = new List<Payment>(); // Initialize empty list
        }

        public void EnrollInCourse(Course course)
        {
            Enrollment enrollment = new Enrollment(Enrollments.Count + 1, this, course, DateTime.Now);
            Enrollments.Add(enrollment);
            course.Enrollments.Add(enrollment);
        }

        public void UpdateStudentInfo(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void MakePayment(decimal amount, DateTime paymentDate)
        {
            Payment payment = new Payment(PaymentHistory.Count + 1, this, amount, paymentDate);
            PaymentHistory.Add(payment);
        }

        public void DisplayStudentInfo()
        {
            Console.WriteLine($"Student ID: {StudentId}, Name: {FirstName} {LastName}, DOB: {DateOfBirth.ToShortDateString()}, Email: {Email}, Phone: {PhoneNumber}");
        }

        public List<Course> GetEnrolledCourses()
        {
            List<Course> courses = new List<Course>();
            foreach (var enrollment in Enrollments)
            {
                courses.Add(enrollment.Course);
            }
            return courses;
        }

        public List<Payment> GetPaymentHistory()
        {
            return PaymentHistory;
        }
    }

    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string InstructorName { get; set; }
        public List<Enrollment> Enrollments { get; set; } // List of enrollments

        // Constructor
        public Course(int courseId, string courseName, string courseCode, string instructorName)
        {
            CourseId = courseId;
            CourseName = courseName;
            CourseCode = courseCode;
            InstructorName = instructorName;
            Enrollments = new List<Enrollment>(); // Initialize empty list
        }

        public void AssignTeacher(Teacher teacher)
        {
            InstructorName = teacher.FirstName + " " + teacher.LastName;
            teacher.AssignedCourses.Add(this);
        }

        public void UpdateCourseInfo(string courseCode, string courseName, string instructor)
        {
            CourseCode = courseCode;
            CourseName = courseName;
            InstructorName = instructor;
        }

        public void DisplayCourseInfo()
        {
            Console.WriteLine($"Course ID: {CourseId}, Name: {CourseName}, Code: {CourseCode}, Instructor: {InstructorName}");
        }

        public List<Enrollment> GetEnrollments()
        {
            return Enrollments;
        }

        public string GetTeacher()
        {
            return InstructorName;
        }
    }

    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public Student Student { get; set; } // Reference to a Student
        public Course Course { get; set; }   // Reference to a Course
        public DateTime EnrollmentDate { get; set; }

        // Constructor
        public Enrollment(int enrollmentId, Student student, Course course, DateTime enrollmentDate)
        {
            EnrollmentId = enrollmentId;
            Student = student;
            Course = course;
            EnrollmentDate = enrollmentDate;
        }

        public Student GetStudent()
        {
            return Student;
        }

        public Course GetCourse()
        {
            return Course;
        }
    }

    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Course> AssignedCourses { get; set; } // List of assigned courses

        // Constructor
        public Teacher(int teacherId, string firstName, string lastName, string email)
        {
            TeacherId = teacherId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AssignedCourses = new List<Course>(); // Initialize empty list
        }

        public void UpdateTeacherInfo(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void DisplayTeacherInfo()
        {
            Console.WriteLine($"Teacher ID: {TeacherId}, Name: {FirstName} {LastName}, Email: {Email}");
        }

        public List<Course> GetAssignedCourses()
        {
            return AssignedCourses;
        }
    }

    public class Payment
    {
        public int PaymentId { get; set; }
        public Student Student { get; set; } // Reference to a Student
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        // Constructor
        public Payment(int paymentId, Student student, decimal amount, DateTime paymentDate)
        {
            PaymentId = paymentId;
            Student = student; // Initialize with a Student object
            Amount = amount;
            PaymentDate = paymentDate;
        }

        public Student GetStudent()
        {
            return Student;
        }

        public decimal GetPaymentAmount()
        {
            return Amount;
        }

        public DateTime GetPaymentDate()
        {
            return PaymentDate;
        }
    }
}
