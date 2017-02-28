using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;


namespace RegistrarApp
{
  public class CourseTest: IDisposable
  {
    public CourseTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Course.DeleteAll();
      Student.DeleteAll();
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_ZeroOutput()
    {
      //Arrange, Act
      int result = Course.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void OverrideBool_SameCourse_ReturnsEqual()
    {
      //Arrange, Act
      Course firstCourse = new Course("HIST101", "United States History to 1877");
      Course secondCourse = new Course("HIST101", "United States History to 1877");
      //Assert
      Assert.Equal(firstCourse, secondCourse);
    }

    [Fact]
    public void Save_OneCourse_CourseSavedToDatabase()
    {
      //Arrange,Act
      Course testCourse = new Course("HIST101", "United States History to 1877");
      testCourse.Save();
      List<Course> result = Course.GetAll();
      List<Course> verify = new List<Course>{testCourse};
      //Assert
      Assert.Equal(verify, result);
    }

    [Fact]
    public void Save_OneCourse_CourseSavedWithCorrectID()
    {
      //Arrange, Act
      Course testCourse = new Course("HIST101", "United States History to 1877");
      testCourse.Save();
      Course savedCourse = Course.GetAll()[0];

      int output = savedCourse.GetId();
      int expected = testCourse.GetId();
      //Assert
      Assert.Equal(expected, output);
    }

    [Fact]
    public void Find_OneCourseId_ReturnCourseFromDatabase()
    {
      Course testCourse = new Course("HIST101", "United States History to 1877");
      testCourse.Save();
      Course result = Course.Find(testCourse.GetId());

      Assert.Equal(testCourse, result);
    }

    [Fact]
    public void SearchName_Name_ReturnCourseFromDatabase()
    {
      Course testCourse = new Course("United States History to 1877", "HIST101");
      testCourse.Save();

      List<Course> output = Course.SearchName("United States History to 1877");
      List<Course> verify = new List<Course>{testCourse};

      Assert.Equal(verify, output);
    }

    [Fact]
    public void SearchNumber_Number_ReturnCourseFromDatabase()
    {
      Course testCourse = new Course("United States History to 1877", "HIST101");
      testCourse.Save();

      List<Course> output = Course.SearchNumber("HIST101");
      List<Course> verify = new List<Course>{testCourse};

      Assert.Equal(verify, output);
    }

    [Fact]
    public void AddStudent_OneCourse_StudentAddedToJoinTable()
    {
    //Arrange
    Course testCourse = new Course("United States History to 1877", "HIST101");
    testCourse.Save();
    Student testStudent = new Student ("Joe", "Fall 2017");
    Student testStudent2 = new Student ("Kat", "Fall 2017");
    testStudent.Save();
    testStudent2.Save();
    testCourse.AddStudent(testStudent);
    testCourse.AddStudent(testStudent2);

    List<Student> output = testCourse.GetStudents();
    List<Student> verify = new List<Student>{testStudent, testStudent2};

    //Assert
    Assert.Equal(verify, output);
    }
  }
}
