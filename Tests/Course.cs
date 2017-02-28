using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RegistrarApp
{
  public class Course
  {
    private int _id;
    private string _courseName;
    private string _courseNumber;

    public Course(string CourseName, string CourseNumber, int Id = 0)
    {
      _id = Id;
      _courseName = CourseName;
      _courseNumber = CourseNumber;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetCourseName()
    {
      return _courseName;
    }

    public string GetCourseNumber()
    {
      return _courseNumber;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM courses;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public override bool Equals(System.Object otherCourse)
    {
      if (!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherCourse;
        bool idEquality = (this.GetId()== newCourse.GetId());
        bool nameEquality = (this.GetCourseName() == newCourse.GetCourseName());
        bool numberEquality = (this.GetCourseNumber() == newCourse.GetCourseNumber());
        return (idEquality && nameEquality && numberEquality);
      }
    }

    public static List<Course> GetAll()
    {
      List<Course> allCourses = new List<Course>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses;", conn);
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO courses (course_name, course_number) OUTPUT INSERTED.id VALUES (@CourseName, @CourseNumber);", conn);
      cmd.Parameters.Add(new SqlParameter("@CourseName", this.GetCourseName()));
      cmd.Parameters.Add(new SqlParameter("@CourseNumber", this.GetCourseNumber()));
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Course Find(int Id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE id = @CourseId;", conn);
      cmd.Parameters.Add(new SqlParameter("@CourseId", Id));
      SqlDataReader rdr = cmd.ExecuteReader();

      int courseId = 0;
      string courseName = null;
      string courseNumber = null;

      while(rdr.Read())
      {
        courseId = rdr.GetInt32(0);
        courseName = rdr.GetString(1);
        courseNumber = rdr.GetString(2);
      }

      Course foundCourse = new Course(courseName, courseNumber, courseId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundCourse ;
    }

    public static List<Course> SearchName(string searchName)
    {
      List<Course> results = new List<Course>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE course_name = @CourseName", conn);
      cmd.Parameters.Add(new SqlParameter("@CourseName", searchName));
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int Id = rdr.GetInt32(0);
        string Name = rdr.GetString(1);
        string Number = rdr.GetString(2);
        Course newCourse = new Course(Name, Number, Id);
        results.Add(newCourse);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return results;
    }

    public static List<Course> SearchNumber(string searchNumber)
    {
      List<Course> results = new List<Course>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE course_number = @CourseNumber", conn);
      cmd.Parameters.Add(new SqlParameter("@CourseNumber", searchNumber));
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int Id = rdr.GetInt32(0);
        string Name = rdr.GetString(1);
        string Number = rdr.GetString(2);
        Course newCourse = new Course(Name, Number, Id);
        results.Add(newCourse);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return results;
    }

    public void AddStudent(Student newStudent)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO students_courses (student_id, course_id) VALUES (@StudentId, @CourseId);", conn);
      cmd.Parameters.Add(new SqlParameter("@StudentId", newStudent.GetId()));
      cmd.Parameters.Add(new SqlParameter("@CourseId", this.GetId().ToString()));
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Student> GetStudents()
    {
      List<Student> results = new List<Student>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT students.* FROM courses JOIN students_courses ON (courses.id = students_courses.course_id) JOIN students ON (students.id = students_courses.student_id) WHERE courses.id = @CourseId;", conn);
      cmd.Parameters.Add(new SqlParameter("@CourseId", this.GetId()));
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int Id = rdr.GetInt32(0);
        string Name = rdr.GetString(1);
        string EnrollDate = rdr.GetString(2);
        Student newStudent = new Student(Name, EnrollDate, Id);
        results.Add(newStudent);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return results;

    }
  }
}
