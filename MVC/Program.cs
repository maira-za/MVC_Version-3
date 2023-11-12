// See https://aka.ms/new-console-template for more information
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Diagnostics;

using (var context = new MyDbContext())
{
    // 1. Create (Add new Students)

    Console.WriteLine("\n\n\t 1. Create --> Add new Students\n\n");

    var student1 = new Student
    {
        FirstMidName = "Atyia",
        LastName = "Alam",
        Email = "atiya@gmail.com",
        PhoneNumber = "0321-23415678",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    var student2 = new Student
    {
        FirstMidName = "Ali",
        LastName = "Ahmed",
        Email = "ali@gmail.com",
        PhoneNumber = "0311-21352478",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    var student3 = new Student
    {
        FirstMidName = "Ayesha",
        LastName = "Khan",
        Email = "ayesha@gmail.com",
        PhoneNumber = "0301-12342138",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    var student4 = new Student
    {
        FirstMidName = "Amna",
        LastName = "Ahmed",
        Email = "amna@gmail.com",
        PhoneNumber = "0300-7651248",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    context.Students?.Add(student1);
    context.Students?.Add(student2);
    context.Students?.Add(student3);
    context.Students?.Add(student4);

    context.SaveChanges();

    Console.WriteLine("\n------------------------------------------");
    // 2. Read (Retrieve all Students)

    var students = (from s in context.Students
                    orderby s.FirstMidName
                    select s).ToList<Student>();

    Console.WriteLine("\n\n\t 2. Read --> Retrieve all Students\n\n");

    foreach (var stdnt in students)
    {
        string name = stdnt.FirstMidName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}", stdnt.ID, name);
    }

    Console.WriteLine("\n------------------------------------------");

    // 3. Update (Modify a Student)

    Console.WriteLine("\n\n\t 3. Update --> Modify a Student\n\n");

    Console.WriteLine("Enter the Last Name of a student you want to update:  ");
    string? up_name = Console.ReadLine();

    Console.WriteLine("Enter the updated values:  ");

    Console.Write("FirstMid Name:  ");
    string? up_firstMidName = Console.ReadLine();

    Console.Write("Last Name:  ");
    string? up_lastName = Console.ReadLine();

    Console.Write("Email:  ");
    string? up_email = Console.ReadLine();

    Console.Write("Phone number:  ");
    string? up_number = Console.ReadLine();

    var studentToUpdate = context.Students?.FirstOrDefault(s => s.LastName == up_name);

    if (studentToUpdate != null)
    {
        studentToUpdate.FirstMidName = up_firstMidName;
        studentToUpdate.LastName = up_lastName;
        studentToUpdate.Email = up_email;
        studentToUpdate.PhoneNumber = up_number;

        context.SaveChanges();
    }
    var up_students = (from s in context.Students
                       where s.LastName == up_lastName
                       orderby s.FirstMidName
                       select s).ToList<Student>();
    
    Console.WriteLine("\n\n\t 2. Read --> Retrieve updated Student\n\n");

    foreach (var stdnt in up_students)
    {
        string name = stdnt.FirstMidName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}", stdnt.ID, name);
    }

    Console.WriteLine("\n------------------------------------------");


    // 4. Delete (Remove a Student)

    Console.WriteLine("\n\n\t 4. Delete --> Remove a Student\n\n");

    Console.WriteLine("Enter the Last Name of a student you want to delete:  ");
    string? del_name = Console.ReadLine();
    var studentToDelete = context.Students?.FirstOrDefault(s => s.LastName == del_name);

    if (studentToDelete != null)
    {
        context.Students?.Remove(studentToDelete);
        context.SaveChanges();
    }

    var del_students = (from s in context.Students
                    orderby s.FirstMidName
                    select s).ToList<Student>();

    Console.WriteLine("\n\n\t 2. Read --> Retrieve all Students after deleting\n\n");

    foreach (var stdnt in del_students)
    {
        string name = stdnt.FirstMidName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}", stdnt.ID, name);
    }

    Console.WriteLine("\n------------------------------------------");

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}
public enum Grade
{
    A, B, C, D, F
}
public class Enrollment
{
    public int EnrollmentID { get; set; }
    public int CourseID { get; set; }
    public int StudentID { get; set; }
    public Grade? Grade { get; set; }

    public virtual Course? Course { get; set; }
    public virtual Student? Student { get; set; }
}

public class Student
{
    public int ID { get; set; }
    public string? LastName { get; set; }
    public string? FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }

    // Add 2 more fields
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public virtual ICollection<Enrollment>? Enrollments { get; set; }
}

public class Course
{
    public int CourseID { get; set; }
    public string? Title { get; set; }
    public int Credits { get; set; }

    public virtual ICollection<Enrollment>? Enrollments { get; set; }
}

public class MyDbContext: DbContext
{
    public virtual DbSet<Course>? Courses { get; set; }
    public virtual DbSet<Enrollment>? Enrollments { get; set; }
    public virtual DbSet<Student>? Students { get; set; }
}


