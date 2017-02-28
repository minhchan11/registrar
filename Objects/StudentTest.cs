using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;


namespace RegistrarApp
{
  public class StudentTest: IDisposable
  {
    public StudentTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Student.DeleteAll();
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_ZeroOutput()
    {
      //Arrange, Act
      int result = Student.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void OverrideBool_SameStudent_ReturnsEqual()
    {
      //Arrange, Act
      Student studentOne = new Student ("Joe", "Fall 2017");
      Student studentTwo = new Student ("Joe", "Fall 2017");

      //Assert
      Assert.Equal(studentTwo, studentOne);
    }

    [Fact]
    public void Save_OneStudent_StudentSavedToDatabase()
    {
      //Arrange
      Student testStudent = new Student ("Joe", "Fall 2017");

      //Act
      testStudent.Save();
      List<Student> output = Student.GetAll();
      List<Student> verify = new List<Student>{testStudent};

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void Save_OneStudent_StudentSavedWithCorrectID()
    {
      //Arrange
      Student testStudent = new Student ("Joe", "Fall 2017");
      testStudent.Save();
      Student savedStudent = Student.GetAll()[0];

      //Act
      int output = savedStudent.GetId();
      int verify = testStudent.GetId();

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void SaveGetAll_ManyStudents_ReturnListOfStudents()
    {
      //Arrange
      Student studentOne = new Student ("Joe", "Fall 2017");
      studentOne.Save();
      Student studentTwo = new Student ("Roy", "Summer 2017");
      studentTwo.Save();

      //Act
      List<Student> output = Student.GetAll();
      List<Student> verify = new List<Student>{studentOne, studentTwo};

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void Find_OneStudentId_ReturnStudentFromDatabase()
    {
      //Arrange
      Student testStudent = new Student ("Joe", "Fall 2017");
      testStudent.Save();

      //Act
      Student foundStudent = Student.Find(testStudent.GetId());

      //Assert
      Assert.Equal(testStudent, foundStudent);
    }

    [Fact]
    public void SearchName_Name_ReturnStudentFromDatabase()
    {
      //Arrange
      Student testStudent = new Student ("Joe", "Fall 2017");
      testStudent.Save();

      //Act
      List<Student> output = Student.SearchName("Joe");
      List<Student> verify = new List<Student>{testStudent};

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void SearchEnrollDate_EnrollDate_ReturnStudentFromDatabase()
    {
      //Arrange
      Student testStudent = new Student ("Joe", "Fall 2017");
      testStudent.Save();

      //Act
      List<Student> output = Student.SearchEnrollDate("Fall 2017");
      List<Student> verify = new List<Student>{testStudent};

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void AddCourse_OneStudent_CourseAddedToJoinTable()
    {
      //Arrange
      Student testStudent = new Student ("Joe", "Fall 2017");
      testStudent.Save();
      Course testCourse = new Course("HIST101", "United States History to 1877");
      testCourse.Save();
      testStudent.AddCourse(testCourse);

      //Act
      List<Course> output = testStudent.GetCourse();
      List<Course> verify = new List<Course>{testCourse};

      //Assert
      Assert.Equal(verify, output);
    }

  }
}
