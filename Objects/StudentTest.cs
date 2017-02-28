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

  }
}
