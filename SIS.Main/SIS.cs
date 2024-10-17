using SIS.Entity;
using SIS.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Main
{
    public class SIS
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Payment> Payments { get; set; }

        // Constructor
        public SIS()
        {
            Students = new List<Student>();
            Courses = new List<Course>();
            Enrollments = new List<Enrollment>();
            Teachers = new List<Teacher>();
            Payments = new List<Payment>();
        }

        public void EnrollStudentInCourse(Student student, Course course)
        {
            student.EnrollInCourse(course);
        }

        public void AssignTeacherToCourse(Teacher teacher, Course course)
        {
            course.AssignTeacher(teacher);
        }

        public void RecordPayment(Student student, decimal amount, DateTime paymentDate)
        {
            student.MakePayment(amount, paymentDate);
        }

        public void GenerateEnrollmentReport(Course course)
        {
            Console.WriteLine($"Enrollment Report for Course: {course.CourseName}");
            foreach (var enrollment in course.Enrollments)
            {
                Console.WriteLine($"Student: {enrollment.Student.FirstName} {enrollment.Student.LastName}, Enrollment Date: {enrollment.EnrollmentDate.ToShortDateString()}");
            }
        }

        public void GeneratePaymentReport(Student student)
        {
            Console.WriteLine($"Payment Report for Student: {student.FirstName} {student.LastName}");
            foreach (var payment in student.GetPaymentHistory())
            {
                Console.WriteLine($"Amount: {payment.GetPaymentAmount()}, Date: {payment.GetPaymentDate().ToShortDateString()}");
            }
        }

        public void CalculateCourseStatistics(Course course)
        {
            Console.WriteLine($"Course Statistics for {course.CourseName}:");
            Console.WriteLine($"Number of Enrollments: {course.Enrollments.Count}");
            decimal totalPayments = 0;
            foreach (var enrollment in course.Enrollments)
            {
                totalPayments += enrollment.Student.GetPaymentHistory().Sum(p => p.GetPaymentAmount());
            }
            Console.WriteLine($"Total Payments: {totalPayments}");
        }

        public void AddEnrollment(Student student, Course course, DateTime enrollmentDate)
        {
            // Validate student existence
            if (!Students.Any(s => s.StudentId == student.StudentId))
            {
                throw new StudentNotFoundException("Student not found in the system.");
            }

            // Validate course existence
            if (!Courses.Any(c => c.CourseId == course.CourseId))
            {
                throw new CourseNotFoundException("Course not found in the system.");
            }

            // Create the enrollment
            student.EnrollInCourse(course); // This will throw a DuplicateEnrollmentException if already enrolled
            Enrollment enrollment = new Enrollment(student.Enrollments.Count + 1, student, course, enrollmentDate);
            course.Enrollments.Add(enrollment);
        }

        public void AssignCourseToTeacher(Course course, Teacher teacher)
        {
            // Validate course existence
            if (!Courses.Any(c => c.CourseId == course.CourseId))
            {
                throw new CourseNotFoundException("Course not found in the system.");
            }

            // Validate teacher existence
            if (!Teachers.Any(t => t.TeacherId == teacher.TeacherId))
            {
                throw new TeacherNotFoundException("Teacher not found in the system.");
            }

            teacher.AssignedCourses.Add(course);
            course.AssignTeacher(teacher);
        }

        public void AddPayment(Student student, decimal amount, DateTime paymentDate)
        {
            // Validate student existence
            if (!Students.Any(s => s.StudentId == student.StudentId))
            {
                throw new StudentNotFoundException("Student not found in the system.");
            }

            // Validate payment
            if (amount <= 0)
            {
                throw new PaymentValidationException("Payment amount must be greater than zero.");
            }

            // Create and add the payment
            Payment payment = new Payment(student.PaymentHistory.Count + 1, student, amount, paymentDate);
            student.PaymentHistory.Add(payment);
        }

        public List<Enrollment> GetEnrollmentsForStudent(Student student)
        {
            // Validate student existence
            if (!Students.Any(s => s.StudentId == student.StudentId))
            {
                throw new StudentNotFoundException("Student not found in the system.");
            }

            return student.Enrollments;
        }

        public List<Course> GetCoursesForTeacher(Teacher teacher)
        {
            // Validate teacher existence
            if (!Teachers.Any(t => t.TeacherId == teacher.TeacherId))
            {
                throw new TeacherNotFoundException("Teacher not found in the system.");
            }

            return teacher.AssignedCourses;
        }
    }
}
