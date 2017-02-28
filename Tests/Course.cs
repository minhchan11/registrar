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
  }
}
