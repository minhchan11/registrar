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
  }
}
