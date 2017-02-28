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
  }
}
