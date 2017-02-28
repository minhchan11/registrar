using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RegistrarApp
{
  public class Student
  {
    private int _id;
    private string _name;
    private string _enrollDate;

    public Student(string Name, string EnrollDate, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _enrollDate = EnrollDate;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetEnrollDate()
    {
      return _enrollDate;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM students;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<Student> GetAll()
    {
      List<Student> allstudents = new List<Student>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string enroll_date = rdr.GetString(2);
        Student newStudent = new Student(name, enroll_date, id);
        allstudents.Add(newStudent);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allstudents;
    }

    public override bool Equals(System.Object randomStudent)
    {
      if(!(randomStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) randomStudent;
        bool idEquality = (this.GetId() == newStudent.GetId());
        bool nameEquality = (this.GetName() == newStudent.GetName());
        bool enrollDateEquality = (this.GetEnrollDate() == newStudent.GetEnrollDate());
        return (idEquality && nameEquality && enrollDateEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO students (name, enroll_date) OUTPUT INSERTED.id VALUES (@StudentName, @StudentDate) ;", conn);
      cmd.Parameters.Add(new SqlParameter("@StudentName", this.GetName()));
      cmd.Parameters.Add(new SqlParameter("@StudentDate", this.GetEnrollDate()));
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Student Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE id = @StudentId;", conn);
      cmd.Parameters.Add(new SqlParameter("@StudentId", id.ToString()));
      SqlDataReader rdr = cmd.ExecuteReader();

      int studentId = 0;
      string studentName = null;
      string studentDate = null;

      while (rdr.Read())
      {
        studentId = rdr.GetInt32(0);
        studentName = rdr.GetString(1);
        studentDate = rdr.GetString(2);
      }

      Student foundStudent = new Student(studentName, studentDate, studentId);
      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundStudent;
    }

    public static List<Student> SearchName(string name)
    {
      List<Student> foundStudents = new List<Student>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE name = @StudentName", conn);
      cmd.Parameters.Add(new SqlParameter("@StudentName", name));
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        string studentDate = rdr.GetString(2);
        Student foundStudent = new Student(studentName, studentDate, studentId);
        foundStudents.Add(foundStudent);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundStudents;
    }

    public static List<Student> SearchEnrollDate(string enrollDate)
    {
      List<Student> foundStudents = new List<Student>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE enroll_date = @StudentEnrollDate", conn);
      cmd.Parameters.Add(new SqlParameter("@StudentEnrollDate", enrollDate));
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        string studentDate = rdr.GetString(2);
        Student foundStudent = new Student(studentName, studentDate, studentId);
        foundStudents.Add(foundStudent);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundStudents;
    }

    public void AddCourse(Course newCourse)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO students_courses (student_id, course_id) VALUES (@StudentId, @CourseId);", conn);
      cmd.Parameters.Add(new SqlParameter("@StudentId", this.GetId().ToString()));
      cmd.Parameters.Add(new SqlParameter("@CourseId", newCourse.GetId()));
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Course> GetCourse()
    {
      List<Course> allCourses = new List<Course>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT courses.* FROM students JOIN students_courses ON (students.id = students_courses.student_id) JOIN courses ON (courses.id = students_courses.course_id) WHERE students.id = @StudentId;", conn);
      cmd.Parameters.Add(new SqlParameter("@StudentId", this.GetId().ToString()));
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int Id = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course newCourse = new Course(courseName, courseNumber, Id);
        allCourses.Add(newCourse);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allCourses;
    }
  }
}
